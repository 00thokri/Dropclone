using System.Text;

public interface IFileService
{
    Task<FileEntity> UploadFileAsync(FileRequest filerequest, string folderName);
    Task<ICollection<FileEntity>> GetFilesInFolderAsync(string folderName);
    Task<bool> DeleteFileAsync(string fileName);
    Task<FileEntity> DownloadFileAsync(string filename);

}
public class FileService : IFileService
{
    private readonly IFileRepository fileRepository;
    private readonly IFolderRepository folderRepository;

    public FileService(IFileRepository fileRepository, IFolderRepository folderRepository)
    {
        this.fileRepository = fileRepository;
        this.folderRepository = folderRepository;
    }

    /// <summary>
    /// Asynchronously uploads a file to the specified folder.
    /// </summary>
    /// <param name="fileRequest">The file request containing the file name and content.</param>
    /// <param name="folderName">The name of the folder to upload the file to.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created <see cref="FileEntity"/>.
    /// </returns>
    /// <exception cref="Exception">Thrown when the specified folder is not found.</exception>
    /// <exception cref="ArgumentException">Thrown when the file name is invalid (null, whitespace, or longer than 25 characters).</exception>
    public async Task<FileEntity> UploadFileAsync(FileRequest fileRequest, string folderName)
    {
        var folder = await folderRepository.FindFolderByNameAsync(folderName);
        if (folder == null)
        {
            throw new Exception("Folder not found");
        }

        if (string.IsNullOrWhiteSpace(fileRequest.Name) || fileRequest.Name.Length > 25)
        {
            throw new ArgumentException("Invalid file name");
        }

        if (await fileRepository.FindFileByName(fileRequest.Name) != null)
        {
            throw new ArgumentException("File with that name already exists");
        }

        var file = new FileEntity(folder.Id, fileRequest.Name, fileRequest.Content);
        return await fileRepository.CreateFileAsync(file);
    }
/// <summary>
/// Asynchronously deletes a file 
/// </summary>
/// <param name="fileName">The name of the file to be deleted</param>
/// <returns> Returns the result of the deletion as a boolean</returns>
/// <exception cref="Exception">Thrown if the file to be deleted is not found</exception>
    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var file = await fileRepository.FindFileByName(fileName);
        if (file == null)
        {
            throw new Exception("File not found");
        }

        return await fileRepository.DeleteFileAsync(file);
    }

    /// <summary>
    /// Get a collection of all files found in a folder
    /// </summary>
    /// <param name="folderName">the name of the folder to search</param>
    /// <returns>Returns a list of all files found</returns>
    /// <exception cref="Exception"></exception>
    public async Task<ICollection<FileEntity>> GetFilesInFolderAsync(string folderName)
    {
        var folder = await folderRepository.FindFolderByNameAsync(folderName);
        if (folder == null)
        {
            throw new Exception("Folder not found");
        }

        return await fileRepository.GetAllFilesInFolder(folder.Id);
    }



/// <summary>
/// 
/// </summary>
/// <param name="fileName"></param>
/// <returns>The file that was downloaded</returns>
/// <exception cref="Exception"></exception>
/// <exception cref="ArgumentException"></exception>
    public async Task<FileEntity> DownloadFileAsync(string fileName)
    {
        var file = await fileRepository.FindFileByName(fileName);
        if (file == null)
        {
            throw new Exception("File not found");
        }
        
        char[] invalidChars = Path.GetInvalidFileNameChars();
        if (fileName.Any(c => invalidChars.Contains(c)))
        {
            throw new ArgumentException("File name contains invalid characters.");
        }

        byte[] byteArray = Encoding.UTF8.GetBytes(file.Content);
        //in my case downloads to: D:\Projects\Backend\IndivProjekt\Dropclone\DownloadedFiles\
        var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        var downloadDirectory = Path.Combine(projectRoot, "DownloadedFiles");

        if (!Directory.Exists(downloadDirectory))
        {
            Directory.CreateDirectory(downloadDirectory);
        }
        
        var filePath = Path.Combine(downloadDirectory, file.Name);

        try
        {
            await File.WriteAllBytesAsync(filePath, byteArray);
            Console.WriteLine("Saving file to: " + filePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error saving file: {ex.Message}");
        }
        

        return file;
    }
}
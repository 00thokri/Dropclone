public interface IFileService
{
    Task<FileEntity> UploadFileAsync(FileRequest filerequest, string folderName);
    Task<ICollection<FileEntity>> GetFilesInFolderAsync(string folderName);
    Task<bool> DeleteFileAsync(string fileName);

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

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var file = await fileRepository.FindFileByName(fileName);
        if (file == null)
        {
            throw new Exception("File not found");
        }

        return await fileRepository.DeleteFileAsync(file);
    }

    public async Task<ICollection<FileEntity>> GetFilesInFolderAsync(string folderName)
    {
        var folder = await folderRepository.FindFolderByNameAsync(folderName);
        if (folder == null)
        {
            throw new Exception("Folder not found");
        }

        return await fileRepository.GetAllFilesInFolder(folder.Id);
    }

    public async Task<FileEntity> UploadFileAsync(FileRequest fileRequest, string folderName)
    {
        var folder = await folderRepository.FindFolderByNameAsync(folderName);
        if (folder == null)
        {
            throw new Exception("No folder to add file into");
        }

        var file = new FileEntity(folder.Id, fileRequest.Name, fileRequest.Content);
        return await fileRepository.CreateFileAsync(file);
    }
}
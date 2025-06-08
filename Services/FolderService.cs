public interface IFolderService
{
    Task<FolderEntity> CreateFolderAsync(string name);
    Task<ICollection<FolderEntity>> GetAllFoldersAsync();
    Task<bool> DeleteFolderAsync(string folderName);

}
public class FolderService: IFolderService
{
    private readonly IFolderRepository folderRepository;
    private readonly IFileRepository fileRepository;

    public FolderService(IFolderRepository folderRepository, IFileRepository fileRepository)
    {
        this.folderRepository = folderRepository;
        this.fileRepository = fileRepository;

    }
/// <summary>
/// Asynchronously creates a folder by the specified name
/// </summary>
/// <param name="name">The name of the folder</param>
/// <returns>Returns the created <see cref="FolderEntity"/></returns>
/// <exception cref="ArgumentException">Thrown if the folder name is invalid or already exists</exception>
    public async Task<FolderEntity> CreateFolderAsync(string name)
    {
        
        if(string.IsNullOrWhiteSpace(name) || name.Length > 25)
        {
            throw new ArgumentException("Folder name cannot be null, whitespace, or longer than 25 characters.");
        }

        if(name.Any(char.IsWhiteSpace))
        {
            throw new ArgumentException("Folder name cannot contain whitespace characters.");
           
        }

        if(await folderRepository.FindFolderByNameAsync(name) != null)
        {
            throw new ArgumentException($"Folder with name '{name}' already exists.");
        }

            var folderEntity = await folderRepository.CreateFolderAsync(name);
            return folderEntity;
        
        
    }
    /// <summary>
    /// Asynchronously gets all the folders from the database
    /// </summary>
    /// <returns>Returns a list containing all folders</returns>
    public async Task<ICollection<FolderEntity>> GetAllFoldersAsync()
    {
        var folders = await folderRepository.GetAllFoldersAsync();
        return folders;
    }

/// <summary>
/// Deletes a folder and all files in it specified by the name of the folder
/// </summary>
/// <param name="folderId">The ID of the folder to be deleted</param>
/// <returns>Returns the result of the deletion as a boolean </returns>
    public async Task<bool> DeleteFolderAsync(string folderName)
    {
        
        return await folderRepository.DeleteFolderAsync(folderName);
    }

 
 }
public interface IFolderService
{
    Task<FolderEntity> CreateFolderAsync(string name);
    Task<ICollection<FolderEntity>> GetAllFoldersAsync();
    Task<bool> DeleteFolderAsync(Guid folderId);

}

public class FolderService: IFolderService
{
    private readonly IFolderRepository folderRepository;

    public FolderService(IFolderRepository folderRepository)
    {
        this.folderRepository = folderRepository;

    }

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
    
    public async Task<ICollection<FolderEntity>> GetAllFoldersAsync()
    {
        var folders = await folderRepository.GetAllFoldersAsync();
        return folders;
    }

    public async Task<bool> DeleteFolderAsync(Guid folderId)
    {
        return await folderRepository.DeleteAsync(folderId);
    }

 
 }
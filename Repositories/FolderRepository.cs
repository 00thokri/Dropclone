using Microsoft.EntityFrameworkCore;
public interface IFolderRepository
{
    Task<FolderEntity> CreateFolderAsync(string name);

    Task<ICollection<FolderEntity>> GetAllFoldersAsync();
    Task<bool> DeleteAsync(Guid folderId);
    Task<FolderEntity?> FindFolderByNameAsync(string name);

}

public class EFFolderRepository : IFolderRepository
{
    private readonly AppDbContext context;

    public EFFolderRepository(AppDbContext context)
    {
       this.context = context;
    }

    public async Task<FolderEntity> CreateFolderAsync(string name)
    {
        var folder = new FolderEntity(name);
        context.Folders.Add(folder);
        await context.SaveChangesAsync();
        return folder;
    }

    public async Task<ICollection<FolderEntity>> GetAllFoldersAsync()
    {
        return await context.Folders.Include(f => f.Files).ToListAsync();
    }

    public async Task<FolderEntity?> FindFolderByNameAsync(string name)
    {
        return await context.Folders.FirstOrDefaultAsync(f => f.Name == name);
    }
    

    public async Task<bool> DeleteAsync(Guid folderId)
    {
        var folder = await context.Folders.FindAsync(folderId);
        if (folder == null) return false;

        context.Folders.Remove(folder);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<FolderEntity?> FindFolderByNameId(Guid folderId)
    {
        var folder = await context.Folders.FindAsync(folderId);
        if (folder == null) return null;

        return folder;
    }
}
using Microsoft.EntityFrameworkCore;
public interface IFolderRepository
{
    Task<FolderEntity> CreateFolderAsync(string name);

    Task<ICollection<FolderEntity>> GetAllFoldersAsync();
    Task<bool> DeleteFolderAsync(string folderName);
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
    

    public async Task<bool> DeleteFolderAsync(string folderName)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        var folder = await context.Folders
            .Include(f => f.Files)
            .FirstOrDefaultAsync(f => f.Name == folderName);

        if (folder == null) return false;
        try
        {
            context.Files.RemoveRange(folder.Files);
            context.Folders.Remove(folder);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return false;
        }
     
    }


}
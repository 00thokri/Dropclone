using Microsoft.EntityFrameworkCore;

public interface IFileRepository
{
    Task<FileEntity> CreateFileAsync(FileEntity file);
    Task<ICollection<FileEntity>> GetAllFilesInFolder(Guid folderId);
    Task<bool> DeleteFileAsync(FileEntity file);
    Task<FileEntity?> FindFileByName(string name);
}

public class EFFileRepistory : IFileRepository
{
    private readonly AppDbContext context;

    public EFFileRepistory(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<FileEntity> CreateFileAsync(FileEntity file)
    {
        await context.Files.AddAsync(file);
        await context.SaveChangesAsync();
        return file;
    }

    public async Task<bool> DeleteFileAsync(FileEntity file)
    {
        var fileDB = await context.Files.FindAsync(file.Id);
        if (fileDB == null) return false;

        context.Files.Remove(file);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<FileEntity?> FindFileByName(string name)
    {
        return await context.Files.FirstOrDefaultAsync(f => f.Name == name);
    }

    public async Task<ICollection<FileEntity>> GetAllFilesInFolder(Guid folderId)
    {
        return await context.Files
            .Where(f => f.FolderId == folderId)
            .ToListAsync();
    }
}
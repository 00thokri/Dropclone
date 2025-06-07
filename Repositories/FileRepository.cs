public interface IFileRepository
{
    Task<FileEntity> CreateFileAsync(FileEntity file);
    Task<ICollection<FileEntity>> GetAllFilesInFolder(Guid folderId);
    Task<bool> DeleteFileAsync(Guid fileId);
    Task<FileEntity?> FindFileByName(string name);
}
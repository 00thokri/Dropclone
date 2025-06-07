public class FileEntity
{
    public Guid Id { get; set; }
    public FolderEntity Folder { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;





    public FileEntity(FolderEntity folder, string name, string content)
    {
        this.Id = Guid.NewGuid();
        this.Folder = folder;
        this.Name = name;
        this.Content = content;
    }

    public FileEntity()
    {
        this.Name = string.Empty;
        this.Content = string.Empty;
        this.Folder = null!;
    }
}
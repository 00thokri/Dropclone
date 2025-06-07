public class FileEntity
{
    public Guid Id { get; set; }
    public Guid FolderId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;





    public FileEntity(Guid folderId, string name, string content)
    {
        this.Id = Guid.NewGuid();
        this.FolderId = folderId;
        this.Name = name;
        this.Content = content;
    }

    public FileEntity()
    {
        this.Name = string.Empty;
        this.Content = string.Empty;
    }
}
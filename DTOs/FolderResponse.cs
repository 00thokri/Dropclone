public class FolderResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<FileEntity> Files { get; set; }
    
    public static FolderResponse FromEntity(FolderEntity folder)
    {
        return new FolderResponse
        {
            Id = folder.Id,
            Name = folder.Name,
            Files = folder.Files.ToList()
        };
    }

}
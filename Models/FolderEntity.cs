using Microsoft.AspNetCore.SignalR;
using Npgsql.Replication;

public class FolderEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public ICollection<FileEntity> Files { get; set; }
    public FolderEntity(string name)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Files = [];
    }

    public FolderEntity()
    {
        this.Name = string.Empty;
        this.Files = [];
    }


}


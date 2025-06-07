using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public DbSet<FolderEntity> Folders { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

}


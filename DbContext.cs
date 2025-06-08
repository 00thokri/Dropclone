using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public DbSet<FolderEntity> Folders { get; set; }
    public DbSet<FileEntity> Files { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<FolderEntity>()
        .HasMany(f => f.Files)
        .WithOne()
        .HasForeignKey(f => f.FolderId)
        .OnDelete(DeleteBehavior.Cascade);
}

}


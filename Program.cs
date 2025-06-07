using Microsoft.EntityFrameworkCore;
namespace Dropclone;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString)
        );
        builder.Services.AddControllers();
        builder.Services.AddScoped<IFolderService, FolderService>();
        builder.Services.AddScoped<IFolderRepository, EFFolderRepository>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IFileRepository, EFFileRepistory>();
        
       
         var app = builder.Build();

        app.MapControllers();
        app.Run();
    }
}

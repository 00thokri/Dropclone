using Microsoft.AspNetCore.Mvc;
[ApiController]
public class FolderController : ControllerBase
{
    private readonly IFolderService folderService;

    public FolderController(IFolderService folderService)
    {
        this.folderService = folderService;
    }

    [HttpPost("create/{name}")]
    public async Task<IActionResult> CreateFolderAsync(string name)
    {

        try
        {
            var folder = await folderService.CreateFolderAsync(name);
            return Ok("Folder created successfully: " + folder.Name);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getFolders")]
    public async Task<IActionResult> GetAllFoldersAsync()
    {
        try
        {
            var folders = await folderService.GetAllFoldersAsync();
            return Ok(folders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete/{folderId}")]
    public async Task<IActionResult> DeleteFolderAsync(Guid folderId)
    {
        try
        {
            var result = await folderService.DeleteFolderAsync(folderId);
            if (result)
            {
                return Ok("Folder deleted successfully.");
            }
            else
            {
                return NotFound("Folder not found.");
            }
        }
         catch (Exception exception)
        {
            return StatusCode(500,  exception.Message);
        }
    }

}
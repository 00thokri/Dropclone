using Microsoft.AspNetCore.Mvc;

public class FileController : ControllerBase
{
    private readonly IFileService fileService;

    public FileController(IFileService fileService)
    {
        this.fileService = fileService;
    }

    [HttpPost("upload/{folderName}")]
    public async Task<IActionResult> UploadFileAsync([FromBody] FileRequest file, string folderName)
    {
        try
        {
            var newFile = await fileService.UploadFileAsync(file, folderName);
            return Ok("File created succesfully");
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("getFiles/{folderName}")]
    public async Task<IActionResult> GetFilesInFolderAsync(string folderName)
    {
        try
        {
            var files = await fileService.GetFilesInFolderAsync(folderName);
            return Ok(files);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("deleteFile/{filename}")]
    public async Task<IActionResult> DeleteFile(string filename)
    {
        try
        {
            var result = await fileService.DeleteFileAsync(filename);
            if (result)
            {
                return Ok("File deleted successfully.");
            }
            else
            {
                return NotFound("File not found.");
            }
        }
        catch (Exception exception)
        {
            return StatusCode(500, exception.Message);
        }
    }

    [HttpGet("download/{filename}")]
    public async Task<IActionResult> DownloadFile(string filename)
    {
       try
        {
            var newFile = await fileService.DownloadFileAsync(filename);
            return Ok("File created succesfully");
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        
    }
}
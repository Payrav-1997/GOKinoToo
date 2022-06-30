using Kino.Common.IService;

namespace Kino.Service;

public class FileService  : IFileService
{
    public async Task<string> AddFileAsync(string dir, IFormFile[] file)
    {
        dir = dir.ToLower();
        
      //  var rootDirectory = Environment.GetEnvironmentVariable("WebRootPath")!;
      
      var rootDirectory = Path.GetFullPath("wwwroot/images/");
        var fileDirectory = Path.Combine(rootDirectory, dir);
        if (!Directory.Exists(fileDirectory))
            Directory.CreateDirectory(fileDirectory);

        foreach (var f in file)
        {
            var fileName = string.Concat(DateTime.Now.Ticks, f.FileName);
        
            var filePath = Path.Combine(fileDirectory, fileName);

            await using var fs = new FileStream(filePath, FileMode.Create);
        
            await f.CopyToAsync(fs);
            return Path.Combine("/storage",dir, fileName);
        }

        return null;
    }
}
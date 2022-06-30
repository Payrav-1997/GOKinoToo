namespace Kino.Common.IService;

public interface IFileService
{
    Task<string> AddFileAsync(string dir, IFormFile[] file);
}
using Microsoft.AspNetCore.Http;

namespace Interview.Application.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, int personId);
        void DeleteFile(int personId);
        Stream GetFile(int personId);
        (Stream Stream, string ContentType)? GetFileWithContentType(int personId);
        bool FileExists(int personId);
    }
}

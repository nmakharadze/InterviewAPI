using Interview.Application.Services;
using Interview.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace Interview.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _webRootPath;
        
        public FileStorageService()
        {
            _webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }
        
        public async Task<string> SaveFileAsync(IFormFile file, int personId)
        {
            // წავშალოთ არსებული ფაილები ამ პერსონისთვის
            DeleteFile(personId);
            
            var fileName = GenerateFileName(file, personId);
            var filePath = GetFullFilePath(fileName);
            
            // დირექტორიის შექმნა თუ არ არსებობს
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            
            // ქმნის ახალ ფაილს
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            
            var relativePath = GetRelativePath(fileName);
            
            return relativePath;
        }
        
        public void DeleteFile(int personId)
        {
            var fileName = $"{personId}_profile.*";
            var directory = GetImagesDirectory();
            var files = Directory.GetFiles(directory, fileName);
            
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }
        
        public Stream GetFile(int personId)
        {
            var fileName = $"{personId}_profile.*";
            var directory = GetImagesDirectory();
            var files = Directory.GetFiles(directory, fileName);
            
            if (files.Length == 0)
                return null;
            
            var filePath = files[0];
            if (!File.Exists(filePath))
                return null;
            
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
        
        public (Stream Stream, string ContentType)? GetFileWithContentType(int personId)
        {
            var fileName = $"{personId}_profile.*";
            var directory = GetImagesDirectory();
            var files = Directory.GetFiles(directory, fileName);
            
            if (files.Length == 0)
                return null;
            
            var filePath = files[0];
            if (!File.Exists(filePath))
                return null;
            
            var extension = Path.GetExtension(filePath);
            var contentType = FileExtensionHelper.GetContentType(extension);
            
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return (stream, contentType);
        }
        
        public bool FileExists(int personId)
        {
            var fileName = $"{personId}_profile.*";
            var directory = GetImagesDirectory();
            var files = Directory.GetFiles(directory, fileName);
            return files.Length > 0;
        }
        
        private string GenerateFileName(IFormFile file, int personId)
        {
            var extension = Path.GetExtension(file.FileName);
            return $"{personId}_profile{extension}";
        }
        
        private string GetFullFilePath(string fileName)
        {
            return Path.Combine(GetImagesDirectory(), fileName);
        }
        
        private string GetRelativePath(string fileName)
        {
            return $"/images/persons/{fileName}";
        }
        
        private string GetImagesDirectory()
        {
            return Path.Combine(_webRootPath, "images", "persons");
        }
    }
}

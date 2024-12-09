using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);
        public Task DownloadFileById(int fileName);
    }
}

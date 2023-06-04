using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Shop_MVC_VPD_121.Services
{
    public interface IAzureStorageService
    {
        Task<string> UploadAsync(IFormFile file);
        Task DeleteAsync(string fileName);

    }
    public class AzureStorageService : IAzureStorageService
    {
        private readonly string connStr;
        const string containerName = "images";
        public AzureStorageService(IConfiguration configuration)
        {
            connStr = configuration.GetConnectionString("AzureStorage");
        }
        public Task DeleteAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            BlobContainerClient client = new(connStr, containerName);

            await client.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // generate new name
            string name = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            string newFileName = name + extension;

            // upload file
            var blob = client.GetBlobClient(newFileName);
            await blob.UploadAsync(file.OpenReadStream());

            // return file public URL
            return blob.Uri.ToString();
        }
    }
}

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Configuration;
using System.Web;

namespace ContentVnptApplication.Services
{
    public class CloudinaryService
    {
        private Cloudinary cloudinary;

        public CloudinaryService()
        {
            Account account = new Account(
                 ConfigurationManager.AppSettings["CloudinaryCloudName"],
                 ConfigurationManager.AppSettings["CloudinaryApiKey"],
                 ConfigurationManager.AppSettings["CloudinaryApiSecret"]
            );

            cloudinary = new Cloudinary(account);
        }

        public ImageUploadResult UploadImage(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
                return null;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.InputStream),
                Folder = "contentvnpt/banners"
            };

            var result = cloudinary.Upload(uploadParams);

            System.Diagnostics.Debug.WriteLine("SecureUrl: " + result.SecureUrl);
            System.Diagnostics.Debug.WriteLine("PublicId: " + result.PublicId);

            if (result.Error != null)
            {
                System.Diagnostics.Debug.WriteLine("Cloudinary Error: " + result.Error.Message);
            }

            return result;
        }

        public void DeleteImage(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
                return;

            var deleteParams = new DeletionParams(publicId);

            cloudinary.Destroy(deleteParams);
        }
    }
}
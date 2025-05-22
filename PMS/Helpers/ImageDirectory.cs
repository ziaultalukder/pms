namespace PMS.Helpers
{
    public class ImageDirectory
    {
        public static string CheckDirectory(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnvironment, string path)
        {
            var hostingPath = hostEnvironment.WebRootPath;
            /*var hostingPath = hostEnvironment.ContentRootPath;*/
            var uploadPath = hostingPath + path;

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            return uploadPath;
        }
        public static bool IsFileExists(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnvironment, string path)
        {
            var hostingPath = hostEnvironment.WebRootPath;
            /*var hostingPath = hostEnvironment.ContentRootPath;*/
            var uploadPath = hostingPath + path;
            return System.IO.File.Exists(uploadPath);
        }
        public static void RemoveExistingFile(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnvironment, string imageName, string imagePath)
        {
            var hostingPath = hostEnvironment.WebRootPath;
            /*var hostingPath = hostEnvironment.ContentRootPath;*/
            var uploadPath = hostingPath + imagePath + imageName;

            if (System.IO.File.Exists(uploadPath))
            {
                System.IO.File.Delete(uploadPath);
            }

        }

        public static async Task<bool> SaveImageInDirectory(byte[] imageInBytes, string path, string imagename)
        {
            try
            {
                using (var stream = new FileStream(string.Format(@"{0}{1}", path, imagename), FileMode.Create))
                {
                    await stream.WriteAsync(imageInBytes, 0, imageInBytes.Length);
                    await stream.FlushAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return false;
            }

        }
    }
}

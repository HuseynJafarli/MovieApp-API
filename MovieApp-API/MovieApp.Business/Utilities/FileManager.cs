using Microsoft.AspNetCore.Http;

namespace MovieApp.Business.Utilities
{
    public static class FileManager
    {
        public static string SaveFile(this IFormFile formFile, string rootName, string folderName)
        {
            string fileName = formFile.FileName;

            fileName = fileName.Length > 64
                ? fileName.Substring(fileName.Length - 64, 64)
                : fileName;

            fileName = Guid.NewGuid().ToString() + fileName;

            string path = Path.Combine(rootName, folderName, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return fileName;
        }

        public static void DeleteFile(string rootName, string folderName, string fileName)
        {
            string path = Path.Combine(rootName, folderName, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace Application.Utilites
{
    [Serializable]
    public class ValidFileExtensionException : Exception
    {
        public ValidFileExtensionException() { }

        public ValidFileExtensionException(string message)
            : base(message) { }

        public ValidFileExtensionException(string message, Exception inner)
            : base(message, inner) { }

    }


    public class File
    {

        public async static Task<string> Save(HttpPostedFileBase file, string path, bool UniqueFileName = true)
        {
            return await Task.Run(() =>
            {

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);
                var uniqueId = Guid.NewGuid().ToString();
                string savePath = path + @"\" + filename + uniqueId + fileExtension;

                file.SaveAs(savePath);

                return filename + uniqueId + fileExtension;

            });
        }

        public async static Task<string> Save(HttpPostedFileBase file, string path, bool UniqueFileName = true, params string[] validFileExtensions)
        {
            return await Task.Run(() =>
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                var fileExtension = Path.GetExtension(file.FileName);

                if (validFileExtensions.Contains(fileExtension))
                {
                    var filename = Path.GetFileNameWithoutExtension(file.FileName);
                    var uniqueId = Guid.NewGuid().ToString();
                    string savePath = path + @"\" + filename + uniqueId + fileExtension;
                    file.SaveAs(savePath);
                    return filename + uniqueId + fileExtension;
                }

                throw new ValidFileExtensionException("File Extensions Is Not Valid");
            });
        }

        public static void DeleteFile(string path, string fileName)
        {
            System.IO.File.Delete(path + fileName);
        }
    }


}
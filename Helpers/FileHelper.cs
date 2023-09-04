using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSystem.Helpers
{
    public class FileHelper
    {
        private const string FilesFolder = "Files";

        private static void CheckDirectory(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        public static string SaveProductFile(OpenFileDialog file)
        {
            return SaveFile(file);
        }

        public static string SaveFile(OpenFileDialog file)
        {
            CheckDirectory(Path.Combine(FilesFolder));

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            File.Copy(fileName, $"pack://application:,,,/{FilesFolder}", true);

            return $"pack://application:,,,/{FilesFolder}/{fileName}";
        }
    }
}

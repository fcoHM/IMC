using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.Model.Repositories
{
    public class FileAccesHelper
    {
        public static string GetPathFile(string File)
            =>System.IO.Path.Combine(FileSystem.AppDataDirectory, File);

        public static string GetAppDataDirectory()
            => FileSystem.AppDataDirectory;
    }
}

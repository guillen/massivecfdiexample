using Cfdi.Domain.Services;
using System.IO.Compression;

namespace Cfdi.Worker.Services
{
    internal class CfdiZipCreator : IZipCreator
    {
        public bool AddInvoices(string[] files, string pathToSave, string fileName)
        {
            if (File.Exists(pathToSave + "\\" + fileName))
            {
                return true;
            }

            using (ZipArchive archive = ZipFile.Open(pathToSave + "\\" + fileName, ZipArchiveMode.Create))
            {
                foreach (var fPath in files)
                {
                    archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
                }
            }
            return true;
        }

        public bool AddInvoices(string location, string pathToSave, string fileName)
        {
            ZipFile.CreateFromDirectory(location, pathToSave + "\\" + fileName);
            return true;
        }
    }
}

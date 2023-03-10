using Cfdi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Worker.Services
{
    internal class FileManagerService : IFileManagerService
    {
        public void CopyFile(string from, string to, string type, string name)
        {
            if (!Directory.Exists(to))
            {
                Directory.CreateDirectory(to);
            }

            if (!Directory.Exists(to + "\\" + type))
            {
                Directory.CreateDirectory(to + "\\" + type);
            }

            File.Copy(from, to + "\\" + type + "\\" + name, true);
        }

        public void RemoveFiles(string[] files)
        {
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }
    }
}

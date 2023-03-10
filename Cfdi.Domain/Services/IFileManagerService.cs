using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IFileManagerService
    {
        void RemoveFiles(string[] files);
        void CopyFile(string from, string to, string type, string name);
    }
}

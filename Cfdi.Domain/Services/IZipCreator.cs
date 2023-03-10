using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IZipCreator
    {
        bool AddInvoices(string[] files, string pathToSave, string fileName);
        bool AddInvoices(string location, string pathToSave, string fileName);
    }
}

using Cfdi.Domain.Entity;
using Cfdi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IXmlReader
    {
        CfdiEntity GetCfdi(CfdiHistory cfdi, string location);
    }
}

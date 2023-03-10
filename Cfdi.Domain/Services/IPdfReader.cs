using Cfdi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IPdfReader
    {
        string CreateCfdi(CfdiEntity cfdi, string url);
    }
}

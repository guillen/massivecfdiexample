using Cfdi.Domain.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IThreadManagerService
    {
        public void CreateThread(CfdiRequest cfdiRequest);
        public bool CanCreateThread();
    }
}

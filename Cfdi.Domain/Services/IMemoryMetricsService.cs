using Cfdi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Services
{
    public interface IMemoryMetricsService
    {
        public Metric GetRAMWindowsMetrics();
        public Metric GetDiskWindowsMetrics(string diskName);
    }
}

using Cfdi.Domain.DTOs.Request;
using Cfdi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain
{
    public class Utl
    {
        public static double OperationRAMCost(Metric metric, CfdiRequest request)
        {
            return 10;//MB
        }

        public static double OperationDiskCost(Metric metric, CfdiRequest request)
        {
            return 10;//MB
        }

        public static string GetActualDate()
        {
            return DateTime.Now.ToString("ddMMyyyy_HHmmss");
        }
    }
}

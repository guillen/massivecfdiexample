using Cfdi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Models
{
    public class Queue : EntityBase
    {
        public String Data { get; set; }
        public int Order { get; set; }
    }
}

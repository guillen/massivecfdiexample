using Cfdi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Models
{
    public class CfdiHistory : EntityBase
    {
        public String Clave { get; set; }
        public String Poliza { get; set; }
        public String Inciso { get; set; }
        public String TipoDocumento { get; set; }
        public String Endoso { get; set; }
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public String AgenteId { get; set; }
        public String FechaInicioEmisionPoliza { get; set; }
        public String FechaFinEmisionPoliza { get; set; }
        [NotMapped]
        public String Usuario { get; set; }
    }
}

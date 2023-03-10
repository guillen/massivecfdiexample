using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.DTOs.Request
{
    public class CfdiRequest
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
        [Required]
        public String Usuario { get; set; }
    }
}

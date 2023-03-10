using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Domain.Entity
{
    public class CfdiEntity
    {
        public String Emisor { get; set; }
        public String RFCEmisor { get; set; }
        public String Receptor { get; set; }
        public String RFCReceptor { get; set; }
        public String MetodoPago { get; set; }
        public String TipoDeComprobante { get; set; }
        public String Moneda { get; set; }
        public String Descuento { get; set; }
        public String SubTotal { get; set; }
        public String Total { get; set; }
        public String FormaPago { get; set; }
        public String Version { get; set; }
        public String Folio { get; set; }
        public String Fecha { get; set; }
        public String Serie { get; set; }
        public String TipoCambio { get; set; }
        public List<CfdiEntityLine> Conceptos { get; set; }
    }

    public class CfdiEntityLine
    {
        public String ClaveProdServ { get; set; }
        public String Cantidad { get; set; }
        public String Unidad { get; set; }
        public String ValorUnitario { get; set; }
        public String Importe { get; set; }
        public String Descripcion { get; set; }
    }
}

using Cfdi.Domain.Services;
using System.Xml;
using Cfdi.Domain.Entity;
using Cfdi.Domain.Models;
using Cfdi.Worker.Exceptions;
using System.Reflection;

namespace Cfdi.Worker.Services
{
    public class CfdiXmlReader : IXmlReader
    {
        public CfdiEntity GetCfdi(CfdiHistory cfdi, string location)
        {
            CfdiEntity cfdiInvoice = new CfdiEntity();

            //read xml
            using (XmlTextReader reader = new XmlTextReader(location))
            {
                //moverse hasta encontrar el primer elemento (en este caso comprobante)
                reader.Read();
                while (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                }

                //si no es comprobante, la estructura está mal (revisar la regla para cada uno de los cfdis)
                if (!IsValidNode(reader.Name, "comprobante"))
                {
                    throw new XmlReaderException("El nodo raiz " + reader.Name + " no es valido", location);
                }

                //leer datos de la cabecera
                while (reader.MoveToNextAttribute())
                {
                    SetAttribute(reader.Name, reader.Value, cfdiInvoice);
                }

                //buscar emisor, receptor, y conceptos
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (IsValidNode(reader.Name, "emisor"))
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (IsValidAttribute(reader.Name, "rfc"))
                                {
                                    cfdiInvoice.RFCEmisor = reader.Value;
                                }
                                if (IsValidAttribute(reader.Name, "nombre"))
                                {
                                    cfdiInvoice.Emisor = reader.Value;
                                }
                            }
                        }
                        if (IsValidNode(reader.Name, "receptor"))
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (IsValidAttribute(reader.Name, "rfc"))
                                {
                                    cfdiInvoice.RFCReceptor = reader.Value;
                                }
                                if (IsValidAttribute(reader.Name, "nombre"))
                                {
                                    cfdiInvoice.Receptor = reader.Value;
                                }
                            }
                        }
                        if (IsValidNode(reader.Name, "conceptos"))
                        {
                            readConcepts(reader, cfdiInvoice);
                        }
                    }
                }
            }

            return cfdiInvoice;
        }

        private void SetAttribute(string name, string value, CfdiEntity cfdiInvoice)
        {
            PropertyInfo p = cfdiInvoice.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (p != null)
            {
                p.SetValue(cfdiInvoice, value);
            }
        }

        private void SetAttributeLine(string name, string value, CfdiEntityLine cfdiInvoiceLine)
        {
            PropertyInfo p = cfdiInvoiceLine.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (p != null)
            {
                p.SetValue(cfdiInvoiceLine, value);
            }
        }

        private bool IsValidNode(string node, string value)
        {
            return node.ToLower().Equals(value) || node.ToLower().Equals("cfdi:" + value);
        }

        private bool IsValidAttribute(string attribute, string value)
        {
            return attribute.ToLower().Equals(value);
        }

        private void readConcepts(XmlTextReader reader, CfdiEntity cfdiInvoice)
        {
            XmlReader conceptos = reader.ReadSubtree();
            cfdiInvoice.Conceptos = new List<CfdiEntityLine>();

            while (true)
            {
                if (!conceptos.Read())
                {
                    break;
                }

                if (conceptos.NodeType == XmlNodeType.Element)
                {
                    if (IsValidNode(conceptos.Name, "conceptos"))
                    {
                        continue;
                    }

                    if (IsValidNode(conceptos.Name, "concepto"))
                    {
                        CfdiEntityLine line = new CfdiEntityLine();
                        while (conceptos.MoveToNextAttribute())
                        {
                            SetAttributeLine(conceptos.Name, conceptos.Value, line);
                        }
                        cfdiInvoice.Conceptos.Add(line);
                    }
                }
            }
        }
    }
}

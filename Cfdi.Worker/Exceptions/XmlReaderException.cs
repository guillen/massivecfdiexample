using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Worker.Exceptions
{
    internal class XmlReaderException : Exception
    {
        public XmlReaderException(string message, string file) : base(file + ": " +message) { }
    }
}

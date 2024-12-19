using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTexture.Exceptions
{
    class LogaritmoException : Exception
    {
        public LogaritmoException() : base("Invalid Logaritmo.") { }
        public LogaritmoException(string message) : base(message) { }
        public LogaritmoException(string message, Exception innerException) : base(message, innerException) { }
        public LogaritmoException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

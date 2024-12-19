using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTexture.Exceptions
{
    class InvalidMagicException : Exception
    {
        public InvalidMagicException() : base("Invalid SHTX header.") { }
        public InvalidMagicException(string message) : base(message) { }
        public InvalidMagicException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidMagicException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
             
    }
}

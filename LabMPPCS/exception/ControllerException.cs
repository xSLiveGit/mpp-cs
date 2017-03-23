using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LabMppCsharp.utils.exceptions
{
    class ControllerException : Exception
    {
        public ControllerException()
        {
        }

        public ControllerException(string message) : base(message)
        {
        }

        public ControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ControllerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

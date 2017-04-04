using System;
using System.Runtime.Serialization;

namespace SellTicketsModel.exception
{
    public class ControllerException : Exception
    {
        public ControllerException()
        {
        }

        public ControllerException(Exception e) : base(e.Message)
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

using System;
using System.Runtime.Serialization;

namespace SellTicketsModel.exception
{
    class EntityArgumentException : ArgumentException
    {
        public EntityArgumentException()
        {
        }

        public EntityArgumentException(string message) : base(message)
        {
        }

        public EntityArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EntityArgumentException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }

        public EntityArgumentException(string message, string paramName) : base(message, paramName)
        {
        }

        protected EntityArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

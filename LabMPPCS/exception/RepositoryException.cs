using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LabMppCsharp.utils.exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(Exception e):base(e.Message)
        {
          
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPPCS.Validator
{
    public interface IValidator<T>
    {
        //throw RepositoryException
        void Validate(T item);
    }
}

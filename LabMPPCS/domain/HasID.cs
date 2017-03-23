using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LabMPPCS.domain
{
    public interface IHasId<T>
    {
        T Id { get; set; }
    }
}

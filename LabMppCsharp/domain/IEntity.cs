using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMppCsharp.domain
{
    public interface IEntity<ID>
    {
        ID Id { get; set; }
    }
}

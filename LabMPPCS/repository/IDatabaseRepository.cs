using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMPPCS.domain;

namespace LabMPPCS.repository
{
    interface IDatabaseRepository<T, in TId> : IRepository<T,TId> where T : IHasId<TId>
    {
        IList<T> GetAllByProperties(Dictionary<String, String> dictionary);
    }
}

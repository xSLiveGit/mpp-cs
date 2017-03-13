using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMppCsharp.repositories
{
    interface IRepository<T,ID>
    {
        IList<T> GetAll();
        T FindById(ID id);
        void Update(T element);
        void Add(T element);
        T Delete(ID id);
        Int32 GetSize();
        void Clear();
    }
}

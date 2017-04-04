using System;
using System.Collections.Generic;
using SellTicketsModel.entity;

namespace persistence.repository
{
    public interface IRepository<T, in TId> where T : IHasId<TId>
    {
        IList<T> GetAll();
        void Clear();
        void Add(T item);
        void Update(T item);
        Int32 Size();
        T Delete(TId id);
        T FindById(TId id);
    }
}

using System;
using System.Collections.Generic;
using SellTicketsModel.entity;

namespace persistence.repository
{
    interface IDatabaseRepository<T, in TId> : IRepository<T, TId> where T : IHasId<TId>
    {
        IList<T> GetAllByProperties(Dictionary<String, String> dictionary);
    }
}

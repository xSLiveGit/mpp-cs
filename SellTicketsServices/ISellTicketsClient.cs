using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellTicketsModel.entity;

namespace SellTicketsServices
{
    public interface ISellTicketsClient
    {
        void ShowUpdates(Match match);

    }
}

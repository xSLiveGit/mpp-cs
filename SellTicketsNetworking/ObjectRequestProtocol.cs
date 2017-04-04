using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellTicketsNetworking
{
    public interface Request
    {
    }


    [Serializable]
    public class LoginRequest : Request
    {
        public LoginRequest(UserDTO user)
        {
            this.User = user;
        }

        public virtual UserDTO User { get; }
    }

    [Serializable]
    public class LogoutRequest : Request
    {
        public LogoutRequest(UserDTO user)
        {
            this.User = user;
        }

        public virtual UserDTO User { get; }
    }

    [Serializable]
    public class SellTicketsRequest : Request
    {
        public SellTicketsRequest(SalesDTO sale)
        {
            this.Sale = sale;
        }

        public virtual SalesDTO Sale { get; }
    }

    [Serializable]
    public class GetAllRequest : Request
    {
    }

    [Serializable]
    public class GetAllFilteredAndSortedRequest : Request
    {
    }
}

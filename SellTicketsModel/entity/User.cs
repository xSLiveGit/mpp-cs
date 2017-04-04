using System;

namespace SellTicketsModel.entity
{
    public class User : IHasId<String>
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public String Username { get; set; }
        public String Password { get; set; }

        public string Id
        {
            get { return this.Username; }
            set { this.Username = value; }
        }

        protected bool Equals(User other)
        {
            return string.Equals(Username, (string) other.Username) && string.Equals(Password, (string) other.Password);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((User)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Username?.GetHashCode() ?? 0) * 397) ^ (Password?.GetHashCode() ?? 0);
            }
        }
    }
}

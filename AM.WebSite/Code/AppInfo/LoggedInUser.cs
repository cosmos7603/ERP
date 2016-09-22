using System;

namespace AM.WebSite
{
    public class LoggedInUser
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string UserName { get; set; }
        public DateTime LastActionDate { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LoggedInUser)obj);
        }

        protected bool Equals(LoggedInUser other)
        {
            return UserId == other.UserId;
        }

        public override int GetHashCode()
        {
            return UserId;
        }
    }
}
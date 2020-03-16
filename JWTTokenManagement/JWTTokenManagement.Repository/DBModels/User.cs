using System;
using System.Collections.Generic;

namespace JWTTokenManagement.Repository.DBModels
{
    public partial class User
    {
        public User()
        {
            RefreshToken = new HashSet<RefreshToken>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace JWTTokenManagement.Repository.DBModels
{
    public partial class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string Token { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}

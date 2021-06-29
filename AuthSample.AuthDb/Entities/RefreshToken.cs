using System;
using System.Collections.Generic;
using System.Text;

namespace AuthSample.AuthDb.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public Guid Value { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}

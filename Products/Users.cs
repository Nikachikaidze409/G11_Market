using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDTO
{
    internal class Users
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}

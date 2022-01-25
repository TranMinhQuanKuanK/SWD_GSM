﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataAcessLayer.Models
{
    public partial class User
    {
        public User()
        {
            UserBrands = new HashSet<UserBrand>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual ICollection<UserBrand> UserBrands { get; set; }
    }
}
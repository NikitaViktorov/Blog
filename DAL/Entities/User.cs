using DAL.Enums;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Role Role { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}

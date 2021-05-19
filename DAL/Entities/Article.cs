using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Article()
        {
            this.Comments = new List<Comment>();
        }
    }
}

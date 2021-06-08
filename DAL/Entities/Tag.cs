using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public ICollection<Article> Articles { get; set; }
        public Tag()
        {
            this.Articles = new List<Article>();
        }
    }
}

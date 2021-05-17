using System;


namespace DAL.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}

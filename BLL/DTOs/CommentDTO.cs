using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Text { get; set; }
        public Guid ArticleId { get; set; }
        public ArticleDTO Article { get; set; }
        public UserDTO User { get; set; }
        public Guid UserId { get; set; }
    }
}

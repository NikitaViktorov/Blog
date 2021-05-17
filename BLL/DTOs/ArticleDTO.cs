using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class ArticleDTO
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public UserDTO User { get; set; }
        public ICollection<TagDTO> Tags { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        //public Guid Id { get; set; }
        //public string Title { get; set; }
        //public string Text { get; set; }
        //public ICollection<TagDTO> Tags { get; set; }
        //public Guid UserId { get; set; }
        //public UserDTO User { get; set; }
        //public ICollection<CommentDTO> Comments { get; set; }
        //public ArticleDTO()
        //{
        //    this.TagDTOs = new List<TagDTO>();
        //    this.CommentDTOs = new List<CommentDTO>();
        //}
    }
}

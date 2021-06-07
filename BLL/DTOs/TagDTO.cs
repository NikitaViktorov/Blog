using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class TagDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Text { get; set; }
        public ICollection<ArticleDTO> Articles { get; set; }
    }
}

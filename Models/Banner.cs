using System;
using System.ComponentModel.DataAnnotations;

namespace ContentVnptApplication.Models
{
    public class Banner : BaseModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public string PublicId { get; set; }

        public int DisplayOrder { get; set; } = 0;
    }
}
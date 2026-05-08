using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentVnptApplication.Models
{
    public class Post : BaseModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Không được để trống")]
        [StringLength(255,ErrorMessage ="Không được quá 255 kí tự")]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PostBrief { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Author { get; set; }
        public bool IsPublished { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int PostCategoryId { get; set; }
        public virtual PostCategory PostCategory { get; set;}
    }
}
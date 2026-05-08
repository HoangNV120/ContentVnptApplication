using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentVnptApplication.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostBrief { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Author { get; set; }
        public bool IsPublished { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Created { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int PostCategoryId { get; set; }
        public string PostCategoryTitle { get; set; }
    }
}
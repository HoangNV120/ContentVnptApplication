using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ContentVnptApplication.ViewModels
{
    public class BannerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        [Display(Name = "Thứ tự")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Hiển thị")]
        public bool IsActive { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public string ImageUrl { get; set; }
    }
}
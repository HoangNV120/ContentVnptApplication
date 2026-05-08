using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentVnptApplication.Models
{
    public class PostCategory : BaseModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

    }
}
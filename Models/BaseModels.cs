using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentVnptApplication.Models
{
    public class BaseModels
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set;}
    }
}
using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public Guid? ParentMenuItemId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool OpenInNewTab { get; set; }

        // Navigation Properties
        public MenuItem? ParentMenuItem { get; set; }
        public ICollection<MenuItem> SubMenuItems { get; set; } = new List<MenuItem>();
    }
}

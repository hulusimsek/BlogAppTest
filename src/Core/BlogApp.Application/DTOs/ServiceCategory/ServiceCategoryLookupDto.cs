using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class ServiceCategoryLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

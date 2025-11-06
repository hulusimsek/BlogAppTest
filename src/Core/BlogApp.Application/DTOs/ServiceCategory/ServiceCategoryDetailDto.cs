using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.ServiceCategory
{
    public class ServiceCategoryDetailDto : ServiceCategoryDto
    {
        public ServiceDetailDto? ServiceDetail { get; set; }
    }
}

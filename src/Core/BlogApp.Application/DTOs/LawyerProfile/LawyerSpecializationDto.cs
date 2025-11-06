using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.LawyerProfile
{
    public class LawyerSpecializationDto
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}

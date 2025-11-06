using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.LawyerProfile
{
    public class CreateLawyerProfileDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;
        public string AboutText { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public List<Guid> SpecializationIds { get; set; } = new();
    }
}

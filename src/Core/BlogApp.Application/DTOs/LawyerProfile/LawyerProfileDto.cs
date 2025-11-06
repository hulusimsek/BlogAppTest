using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.LawyerProfile
{
    public class LawyerProfileDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;
        public string AboutText { get; set; } = string.Empty;
        public List<LawyerSpecializationDto> Specializations { get; set; } = new();
        public List<CareerHistoryDto> CareerHistory { get; set; } = new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.LawyerProfile
{
    public class UpdateLawyerProfileDto : CreateLawyerProfileDto
    {
        public Guid Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Tag
{
    public class UpdateTagDto : CreateTagDto
    {
        public Guid Id { get; set; }
    }
}

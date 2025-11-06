using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Contact
{
    public class WorkingHourDto
    {
        public string DayNameTr { get; set; } = string.Empty;
        public string DayNameEn { get; set; } = string.Empty;
        public string Opens { get; set; } = string.Empty;
        public string Closes { get; set; } = string.Empty;
        public bool IsClosed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSAdminModels
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public Enums.SessionPeriod Period { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int MaxChildren { get; set; }

        public List<Class> Classes { get; set; }
    }
}

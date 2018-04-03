using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Data.VBSAdminModels
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public Enums.SessionPeriod Period { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Required]
        public int MaxChildren { get; set; }

        public List<Classroom> Classes { get; set; }

        public List<Child> Children { get; set; }

        [Required]
        public int VBSId { get; set; }
        public VBS VBS { get; set; }
    }
}

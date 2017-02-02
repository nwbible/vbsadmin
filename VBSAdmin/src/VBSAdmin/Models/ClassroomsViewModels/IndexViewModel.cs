using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VBSAdmin.Data.VBSAdminModels;
    

namespace VBSAdmin.Models.ClassroomViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Enums.ClassGrade Grade { get; set; }

        [Required]
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int ChildCount { get; set; }
    }
}


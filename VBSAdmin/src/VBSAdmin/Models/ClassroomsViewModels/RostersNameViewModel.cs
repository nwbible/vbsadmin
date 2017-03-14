using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VBSAdmin.Data.VBSAdminModels;
    

namespace VBSAdmin.Models.ClassroomViewModels
{
    public class RostersNameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Enums.ClassGrade Grade { get; set; }

        public Enums.SessionPeriod SessionPeriod { get; set; }

        public List<string> ChildrenNames { get; set; }
    }
}


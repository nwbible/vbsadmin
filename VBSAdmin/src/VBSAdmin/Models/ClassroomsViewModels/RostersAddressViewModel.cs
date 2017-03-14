using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VBSAdmin.Data.VBSAdminModels;
    

namespace VBSAdmin.Models.ClassroomViewModels
{
    public class RostersAddressViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Enums.ClassGrade Grade { get; set; }

        public Enums.SessionPeriod SessionPeriod { get; set; }

        public List<RostersAddressChildrenViewModel> Children { get; set; }
    }

    public class RostersAddressChildrenViewModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}


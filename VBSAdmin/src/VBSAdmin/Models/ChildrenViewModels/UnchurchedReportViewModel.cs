using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBSAdmin.Data.VBSAdminModels;

namespace VBSAdmin.Models.ChildrenViewModels
{
    public class UnchurchedReportViewModel
    {
        public int Id { get; set; }

        public string ChildName { get; set; }

        public string Address { get; set; }

        public Enums.ClassGrade GradeCompleted { get; set; }

        public string ChurchSpecified { get; set; }

        public string InvitedBy { get; set; }

        public string GuardianName { get; set; }

        public string GuardianRelationship { get; set; }

        public string GuardianPhone { get; set; }

        public string GuardianEmail { get; set; }
    }

    public class UnchurchedReportViewModel2
    {
        public string GuardianName { get; set; }
        public string GuardianPhone { get; set; }
        public string GuardianEmail { get; set; }
        public string GuardianRelationship { get; set; }
        public string Address { get; set; }
        public string InvitedBy { get; set; }
        public string ChurchSpecified { get; set; }
        public string ChildrenNamesAndGrades { get; set; }
    }

}

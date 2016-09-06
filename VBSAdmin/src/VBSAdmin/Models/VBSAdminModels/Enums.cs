using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.VBSAdminModels
{
    public class Enums
    {
        public enum SessionPeriod
        {
            AM = 0,
            PM = 1
        }

        public enum ClassGrade
        {
            ThreeToFourYears=0,
            PreK=1,
            Kindergarten=2,
            First=3,
            Second=4,
            Third=5,
            Fourth=6,
            Fifth=7,
            Sixth=8
        }

        public enum ClassGender
        {
            Boy=0,
            Girl=1,
            Mixed=2
        }

        public enum ChildGender
        {
            Boy=0,
            Girl=1
        }
    }
}

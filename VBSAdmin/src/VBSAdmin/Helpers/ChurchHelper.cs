using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Helpers
{
    public class ChurchHelper
    {
        public static bool IsNoneChurch(string churchName)
        {
            if (string.IsNullOrWhiteSpace(churchName)
                || churchName.ToLower().Trim().StartsWith("none")
                || churchName.ToLower().Trim().StartsWith("n/a")
                || churchName.ToLower().Trim().StartsWith("na")
                || churchName.ToLower().Trim().StartsWith("none at this time")
                || churchName.ToLower().Trim().StartsWith("none currently")
                || churchName.ToLower().Trim().StartsWith("not sure")
                || churchName.ToLower().Trim().StartsWith("still looking")
                || churchName.ToLower().Trim().StartsWith("still seeking")
                || churchName.ToLower().Trim().StartsWith("undecided")
                || churchName.ToLower().Trim().StartsWith("we are looking")
                || churchName.ToLower().Trim().StartsWith("we don't have one")
                || churchName.ToLower().Trim().StartsWith("no current church"))
            {
                return true;
            }

            return false;
        }
    }
}

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
                || churchName.ToLower().Trim().Equals("na")
                || churchName.ToLower().Trim().StartsWith("none at this time")
                || churchName.ToLower().Trim().StartsWith("none currently")
                || churchName.ToLower().Trim().StartsWith("not sure")
                || churchName.ToLower().Trim().StartsWith("still looking")
                || churchName.ToLower().Trim().StartsWith("still seeking")
                || churchName.ToLower().Trim().StartsWith("undecided")
                || churchName.ToLower().Trim().StartsWith("we are looking")
                || churchName.ToLower().Trim().StartsWith("we don't have one")
                || churchName.ToLower().Trim().StartsWith("no current church")
                || churchName.ToLower().Trim().StartsWith("currently moving from va")
                || churchName.ToLower().Trim().StartsWith("looking/visiting")
                || churchName.ToLower().Trim().StartsWith("n:a")
                || churchName.ToLower().Trim().Equals("no")
                || churchName.ToLower().Trim().StartsWith("just moved here")
                || churchName.ToLower().Trim().StartsWith("looking for one")
                || churchName.ToLower().Trim().StartsWith("new to church")
                || churchName.ToLower().Trim().StartsWith("not have one")
                || churchName.ToLower().Trim().StartsWith("we are currently looking")
                || churchName.ToLower().Trim().StartsWith("we dont have one")
                || churchName.ToLower().Trim().StartsWith("looking for a new church")
                || churchName.ToLower().Trim().StartsWith("?")
                || churchName.ToLower().Trim().StartsWith("looking")
                || churchName.ToLower().Trim().StartsWith("we are still looking")
                || churchName.ToLower().Trim().StartsWith("we do not have")
                || churchName.ToLower().Trim().StartsWith("currently looking")
                || churchName.ToLower().Trim().StartsWith("looking for a church")
                || churchName.ToLower().Trim().StartsWith("new to area")
                || churchName.ToLower().Trim().StartsWith("st. thomas (ma) new to oh")
                || churchName.ToLower().Trim().StartsWith("no home church")
                || churchName.ToLower().Trim().StartsWith("good question")
                || churchName.ToLower().Trim().StartsWith("we don't have a church"))
            {
                return true;
            }

            return false;
        }
    }
}

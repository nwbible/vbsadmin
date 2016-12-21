using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin
{
    public static class Constants
    {
        //Claims
        public const string TenantClaim = "Tenant";
        public const string SystemAdminClaim = "SystemAdmin";
        public const string TenantAdminClaim = "TenantAdmin";

        //Cookies
        public const string CurrentVBSIdCookie = "CurrentVBSId";

    }
}

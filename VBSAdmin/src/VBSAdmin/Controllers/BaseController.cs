using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VBSAdmin.Controllers
{
    public class BaseController : Controller
    {
        public Int32 TenantId
        {
            get
            {
                return Convert.ToInt32(Request.Cookies[Constants.TenantClaim]);
            }
        }

        public Int32 CurrentVBSId
        {
            get
            {
                return Convert.ToInt32(Request.Cookies[Constants.CurrentVBSIdCookie]);
            }
        }
        
    }
}
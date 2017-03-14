using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VBSAdmin.Data;
using VBSAdmin.Data.VBSAdminModels;

namespace VBSAdmin.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);

            if (Request.Cookies != null && Request.Cookies[Constants.CurrentVBSIdCookie] != null)
            {
                var vbsContext = _context.VBS.Where(v => v.Id == Convert.ToInt32(Request.Cookies[Constants.CurrentVBSIdCookie])).FirstOrDefault();
                var vbsName = vbsContext.ThemeName;

                ViewData["CurrentVBSName"] = vbsName;
            }
        }

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
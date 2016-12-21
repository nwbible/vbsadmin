using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace VBSAdmin.Authorization
{
    public class BelongsToTenantHandler : AuthorizationHandler<BelongsToTenantRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BelongsToTenantRequirement requirement)
        {

            if (!context.User.HasClaim(c => c.Type == Constants.TenantClaim))
            {
                return Task.CompletedTask;
            }


            //Get the tenant specified in the request cookie.  The cookie is unsecure but it allows us to pass the tenant context back and forth between requests
            //and we validate the requested values here against the actual user claims.
            var atzFilterContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;

            if(atzFilterContext != null)
            {
                string tenantContext = atzFilterContext.HttpContext.Request.Cookies[Constants.TenantClaim];


                if (!String.IsNullOrEmpty(tenantContext) && context.User.HasClaim(Constants.TenantClaim, tenantContext))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;        
        }
    }
}

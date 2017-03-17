using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NetCoreAuth.Mvc.Models;

namespace NetCoreAuth.Mvc.Authorization
{
  public class TodoOwnerHandler : AuthorizationHandler<OwnerRequirement, TodoItem>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement, TodoItem resource)
        {
            var nameClaim = context.User.FindFirst(ClaimTypes.Name);
            if (nameClaim != null && resource.Owner == nameClaim.Value)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
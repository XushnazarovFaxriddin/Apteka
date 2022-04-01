using AptekaAPI.Entitys.Enums;
using AptekaAPI.Models.Admin.PostModel;
using AptekaAPI.Models.Admin.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.JWT
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeAttribute:Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;
        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                // agar harakat [AllowAnonymous] atributi bilan bezatilgan bo'lsa, avtorizatsiyani o'tkazib yuboring
                var allowAnonimus = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
                if (allowAnonimus)
                    return;


                //ruxsat
                var user = (Person)context.HttpContext.Items["Person"];
                if (user is null || (_roles.Any() && !_roles.Contains(user.Role)))
                {
                    
                    //tizimga kirmagan yoki rolga ruxsat berilmagan
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch (Exception ex)
            {
                context.Result = new JsonResult(new { error = ex.Message+ " => AuthorizeAttribute'dan salom." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}

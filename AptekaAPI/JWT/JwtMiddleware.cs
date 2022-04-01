using AptekaAPI.Helpers;
using AptekaAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.JWT
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;            
        }

        public async Task Invoke(HttpContext context, IAdminService adminService, IJwtUtils jwtUtils)
        {
            
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId is not null)
            {
                // muvaffaqiyatli jwt tekshiruvida foydalanuvchini kontekstga biriktiring
                context.Items["Person"] = await adminService.GetByIdAsync(userId.Value);
            }
            await _next(context);
        }
    }
}

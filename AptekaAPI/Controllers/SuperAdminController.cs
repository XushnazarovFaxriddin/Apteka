using AptekaAPI.Entitys.Enums;
using AptekaAPI.Interfaces;
using AptekaAPI.JWT;
using AptekaAPI.Models.Admin.PostModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Role.SuperAdmin)]
    public class SuperAdminController : ControllerBase
    {
        private IAdminService _adminService;
        private IVendorService _vendorService;

        public SuperAdminController(IAdminService adminService, IVendorService vendorService)
        {
            _adminService = adminService;
            _vendorService = vendorService;
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson([FromQuery] int id)
        {
            try
            {
                _adminService.DeletePerson(id);
                return Ok(new { error = false, message = "Muvaffaqiyatli o'chirildi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateRole([FromQuery] int id, [FromQuery] Role role)
        {
            try
            {
                await _adminService.UpdateRoleAsync(id, role);
                return Ok(new { error = false, message = "Foydalanuvchi huquqi o'zgartirildi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromQuery] int id, [FromBody] PersonPost person)
        {
            try
            {
                await _adminService.UpdatePersonAsync(id, person);
                return Ok(new { error = false, message = "Muvaffaqiyatli tahrirlandi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }
    }
}

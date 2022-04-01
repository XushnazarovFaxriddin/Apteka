using AptekaAPI.Entitys.Enums;
using AptekaAPI.Interfaces;
using AptekaAPI.JWT;
using AptekaAPI.Models.Admin.ViewModel;
using AptekaAPI.Models.Vendor.PostModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private IVendorService _vendorService;
        private IAdminService _adminService;

        public VendorController(IVendorService vendorService, IAdminService adminService)
        {
            _vendorService = vendorService;
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginAuth loginModel)
        {
            try
            {
                return Ok(await _vendorService.LoginAsync(loginModel));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Items["Person"] = new Person();
            //await _vendorService.LogoutAsync();
            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicine()
        {
            try
            {                  
                return Ok(await _adminService.GetAllMedicineAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicineById( [FromQuery] int id)
        {
            try
            {               
                return Ok(await _adminService.GetMedicineByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicineType()
        {
            try
            {
                return Ok(await _adminService.GetAllMedicineTypeAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicineTypeId( [FromQuery] int id)
        {
            try
            {
                return Ok(await _adminService.GetMedicineTypeByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountry()
        {
            try
            {
                return Ok(await _adminService.GetAllCountryAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryById( [FromQuery] int id)
        {
            try
            {
                return Ok(await _adminService.GetCountyByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchMedicine(
            string searchText,
            SearchMedicineTypeEnum searchMedicineTypeEnum= SearchMedicineTypeEnum.Name)
        {
            try
            {
                return Ok(await _vendorService.SearchMedicineAsync(searchText, searchMedicineTypeEnum));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }
    }
}

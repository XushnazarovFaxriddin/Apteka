using AptekaAPI.Entitys.Enums;
using AptekaAPI.Interfaces;
using AptekaAPI.JWT;
using AptekaAPI.Models;
using AptekaAPI.Models.Admin.PostModel;
using AptekaAPI.Models.Medicine;
using AptekaAPI.Models.Vendor.PostModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Controllers
{
    [Authorize(Role.Admin, Role.SuperAdmin)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private IVendorService _vendorService;

        public AdminController(IAdminService adminService, IVendorService vendorService)
        {
            _adminService = adminService;
            _vendorService = vendorService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPersonAll()
        {
            try
            {
                return Ok(await _adminService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            try
            {
                return Ok(await _adminService.GetAllAdminsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendor()
        {
            try
            {
                return Ok(await _adminService.GetAllVendorAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterVendor([FromBody] Vendor vendor)
        {
            try
            {
                await _adminService.RegisterAsync(vendor);
                return Ok(new { error = false, message = "Muvaffaqiyatli qo'shildi!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] Country country)
        {
            try
            {
                await _adminService.AddNewCountryAsync(country);
                return Ok(new { error = false, message = "Muvaffaqiyatli qo'shildi!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCountry([FromQuery] int id)
        {
            try
            {
                _adminService.DeleteCountry(id);
                return Ok(new { error = false, message = "Muvaffaqiyatli o'chirildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateVendor([FromQuery] int id, [FromBody] PersonPost person)
        {
            try
            {
                if ((await _adminService.GetByIdAsync(id)).Role != Role.Vendor)
                    throw new Exception("Kechirasiz sizda Administratorlarning malumotlarini o'zgartirishga huquq yoq!");
                await _adminService.UpdatePersonAsync(id, person);
                return Ok(new { error = false, message = "Muvaffaqiyatli tahrirlandi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVendor([FromQuery] int id)
        {
            try
            {
                if ((await _adminService.GetByIdAsync(id)).Role != Role.Vendor)
                    throw new Exception("Kechirasiz sizda Administratorlarning malumotlarini o'zgartirishga huquq yoq!");
                _adminService.DeletePerson(id);
                return Ok(new { error = false, message = "Muvaffaqiyatli o'chirildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicine([FromBody] Medicine medicine)
        {
            try
            {
                await _adminService.AddNewMedicineAsync(medicine);
                return Ok(new { error = false, message = "Muvaffaqiyatli qo'shildi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMedicine([FromQuery] int id)
        {
            try
            {
                _adminService.DeleteMedicine(id);
                return Ok(new { error = false, message = "Muvaffaqiyatli o'chirildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedicine([FromQuery] int id, [FromBody] Medicine medicine)
        {
            try
            {
                await _adminService.UpdateMedicineAsync(id, medicine);
                return Ok(new { error = false, message = "Muvaffaqiyatli tahrirlandi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicineType([FromBody] MedicineType medicineType)
        {
            try
            {
                await _adminService.AddNewMedicineTypeAsync(medicineType);
                return Ok(new { error = false, message = "Muvaffaqiyatli qo'shildi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        } 

        [HttpDelete]
        public async Task<IActionResult> DeleteMedicineType([FromQuery] int id)
        {
            try
            {
                _adminService.DeleteMedicineType(id);
                return Ok(new { error = false, message = "Muvaffaqiyatli o'chirildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMedicineType([FromQuery] int id, [FromBody] MedicineType medicineType)
        {
            try
            {
                await _adminService.UpdateMedicineTypeAsync(id, medicineType);
                return Ok(new { error = false, message = "Muvaffaqiyatli tahrirlandi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }
    }
}

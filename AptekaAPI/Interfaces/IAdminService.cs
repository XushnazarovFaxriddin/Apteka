using AptekaAPI.Entitys.Enums;
using AptekaAPI.Models;
using AptekaAPI.Models.Admin.PostModel;
using AptekaAPI.Models.Admin.ViewModel;
using AptekaAPI.Models.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Interfaces
{
    public interface IAdminService
    {
        Task RegisterAsync(Vendor vendor);

        /// <summary>
        /// Tizim foydalanuvchilari uchun
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Person> GetByIdAsync(int Id);


        /// <summary>
        /// Tizim foydalanuvchilari uchun
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Person>> GetAllAsync();

        Task<IEnumerable<Person>> GetAllAdminsAsync();

        Task<IEnumerable<Person>> GetAllVendorAsync();

        Task AddNewCountryAsync(Country country);

        Task AddNewMedicineAsync(Medicine medicine);

        Task AddNewMedicineTypeAsync(MedicineType medicineType);

        Task ChangePasswordByIdAsync(int id, string newPassword);

        void DeletePerson(int id);

        void DeleteCountry(int id);

        void DeleteMedicine(int id);

        void DeleteMedicineType(int id);

        Task<CountryView> GetCountyByIdAsync(int id);

        Task<IEnumerable<CountryView>> GetAllCountryAsync();

        Task<MedicineView> GetMedicineByIdAsync(int id);

        Task<IEnumerable<MedicineView>> GetAllMedicineAsync();

        Task<MedicineTypeView> GetMedicineTypeByIdAsync(int id);

        Task<IEnumerable<MedicineTypeView>> GetAllMedicineTypeAsync();

        Task UpdateMedicineTypeAsync(int id, MedicineType medicineType);

        Task UpdateMedicineAsync(int id, Medicine medicine);

        Task UpdatePersonAsync(int id, PersonPost person);

        Task UpdateRoleAsync(int id, Role role);
    }
}

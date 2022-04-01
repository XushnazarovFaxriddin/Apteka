using AptekaAPI.Data;
using AptekaAPI.Entitys.Enums;
using AptekaAPI.Helpers;
using AptekaAPI.Interfaces;
using AptekaAPI.JWT;
using AptekaAPI.Models;
using AptekaAPI.Models.Admin.PostModel;
using AptekaAPI.Models.Admin.ViewModel;
using AptekaAPI.Models.Medicine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Services
{
    public class AdminService : IAdminService
    {
        private DataContext _dbContext;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;
        public AdminService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _dbContext = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public async Task<IEnumerable<Person>> GetAllAdminsAsync()
        {
            return (IEnumerable<Person>)_dbContext.Users
                .Where(v => v.Role != Role.Vendor)
                .ToList()
                .ConvertAll(v => toPerson(v));
        }
        private static Person toPerson(Vendor vendor)
        {
            if (vendor is null)
                throw new Exception("Null bo'lishi mumkin emas!");
            return new Person
            {
                Id = vendor.Id,
                FirstName = vendor.FirstName,
                LastName = vendor.LastName,
                PhoneNumber = vendor.PhoneNumber,
                Username = vendor.Username,
                Address = vendor.Address,
                Role = vendor.Role
            };
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var vendors = await _dbContext.Users.ToListAsync();
            return (IEnumerable<Person>)vendors.ConvertAll(v => toPerson(v));
        }

        public async Task<IEnumerable<Person>> GetAllVendorAsync()
        {
            return (IEnumerable<Person>)_dbContext.Users
                .Where(u => u.Role == Role.Vendor)
                .ToList()
                .ConvertAll(v => toPerson(v));
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var vendor = await _dbContext.Users.FirstOrDefaultAsync(v => v.Id == id);
            if (vendor is null)
                throw new Exception("Tizimda bunday foydalanuvchi mavjud emas!");
            return toPerson(vendor);
        }

        public async Task RegisterAsync(Vendor vendor)
        {
            if (vendor is null)
                throw new Exception("Null bo'lishi mumkin emas!");
            vendor.Password = CryptoPassword.Hash(vendor.Password);
            await _dbContext.AddAsync(vendor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewCountryAsync(Country country)
        {
            await _dbContext.Countrys.AddAsync(country);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewMedicineAsync(Medicine medicine)
        {
            await _dbContext.Medicines.AddAsync(medicine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewMedicineTypeAsync(MedicineType medicineType)
        {
            await _dbContext.MedicineTypes.AddAsync(medicineType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangePasswordByIdAsync(int id, string newPassword)
        {
            if (newPassword is null ||
                newPassword.Length > 20 ||
                newPassword.Length < 4)
                throw new Exception("Parol uzunligi 4 tadan 20 tagacha bo'lishi shart.");

            var vendor = await _dbContext.Users.FirstOrDefaultAsync(v => v.Id == id);
            if (vendor is null)
                throw new Exception("Tizimda bunday foydalanuvchi mavjud emas!");

            vendor.Password = CryptoPassword.Hash(newPassword);
            _dbContext.Users.Update(vendor);
        }

        public void DeletePerson(int id)
        {
            var person = _dbContext.Users.FirstOrDefault(v => v.Id == id);

            if (person is null)
                throw new Exception("Tizimda bunday foydalanuvchi mavjud emas!");

            _dbContext.Users.Remove(person);
            _dbContext.SaveChanges();
        }

        public void DeleteCountry(int id)
        {
            var counter = _dbContext.Countrys.FirstOrDefault(c => c.Id == id);
            if (counter is null)
                throw new Exception("Tizimda bunday mamlakat mavjud emas!");
            _dbContext.Countrys.Remove(counter);
            _dbContext.SaveChanges();
        }

        public void DeleteMedicine(int id)
        {
            var medicine = _dbContext.Medicines.FirstOrDefault(m => m.Id == id);
            if (medicine is null)
                throw new Exception("Tizimda bunday dori-darmon mavjud emas!");
            _dbContext.Medicines.Remove(medicine);
            _dbContext.SaveChanges();
        }
        public void DeleteMedicineType(int id)
        {
            var medicineType = _dbContext.MedicineTypes.FirstOrDefault(mt => mt.Id == id);
            if (medicineType is null)
                throw new Exception("Tizimda bunday dori-darmon turi mavjud emas!");
            _dbContext.MedicineTypes.Remove(medicineType);
            _dbContext.SaveChanges();
        }

        public async Task<CountryView> GetCountyByIdAsync(int id)
        {
            var country = await _dbContext.Countrys.FirstOrDefaultAsync(v => v.Id == id);
            if (country is null)
                throw new Exception("Tizimda bunday mamlakat mavjud emas!");
            return new CountryView
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        public async Task<IEnumerable<CountryView>> GetAllCountryAsync()
        {
            IList<CountryView> list = new List<CountryView>(200);
            await _dbContext.Countrys.ForEachAsync(c =>
            {
                list.Add(new CountryView { Id = c.Id, Name = c.Name });
            });
            return (IEnumerable<CountryView>)list;
        }

        public async Task<MedicineView> GetMedicineByIdAsync(int id)
        {
            var medicine = await _dbContext.Medicines.FirstOrDefaultAsync(v => v.Id == id);
            if (medicine is null)
                throw new Exception("Tizimda bunday dori-darmin mavjud emas!");
            return await toMedicineView(medicine);
        }
        private async Task<MedicineView> toMedicineView(Medicine medicine) => new MedicineView
        {
            Id = medicine.Id,
            Name = medicine.Name,
            Discription = medicine.Discription,
            TimeOfProduction = medicine.TimeOfProduction,
            Country = await this.GetCountyByIdAsync(medicine.CountryId),
            BoxPrice = medicine.BoxPrice,
            MedicineType = await this.GetMedicineTypeByIdAsync(medicine.MedicineTypeId),
            HowMany = medicine.HowMany,
            ShelfLife = medicine.ShelfLife
        };

        public async Task<IEnumerable<MedicineView>> GetAllMedicineAsync()
        {
            IList<MedicineView> list = new List<MedicineView>();
            await _dbContext.Medicines.ForEachAsync(async x => list.Add(await toMedicineView(x)));
            return (IEnumerable<MedicineView>)list;
        }

        public async Task<MedicineTypeView> GetMedicineTypeByIdAsync(int id)
        {
            var medicineType = await _dbContext.MedicineTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (medicineType is null)
                throw new Exception("Tizimda bunday tipdagi dori-darmon mavjud emas!");
            return toMedicineTypeView(medicineType);
        }
        private static MedicineTypeView toMedicineTypeView(MedicineType medicineType) => new MedicineTypeView
        {
            Id = medicineType.Id,
            Description = medicineType.Description,
            Title = medicineType.Title
        };

        public async Task<IEnumerable<MedicineTypeView>> GetAllMedicineTypeAsync()
            => (IEnumerable<MedicineTypeView>)_dbContext.MedicineTypes.ToList().ConvertAll<MedicineTypeView>(toMedicineTypeView);


        public async Task UpdateMedicineTypeAsync(int id, MedicineType medicineType)
        {
            var mt = await this.GetMedicineTypeByIdAsync(id);
            medicineType.Id = mt.Id;
            _dbContext.MedicineTypes.Update(medicineType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMedicineAsync(int id, Medicine medicine)
        {
            var medi = await this.GetMedicineByIdAsync(id);
            medicine.Id = medi.Id;
            /*var medicineView = await this.toMedicineView(medicine);
            medicineView.Id = medi.Id;*/
            _dbContext.Medicines.Update(medicine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(int id, PersonPost person)
        {
            var p = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (p is null)
                throw new Exception("Tizimda bunday foydalanuvchi mavjud emas!");
            p.FirstName = person.FirstName;
            p.LastName = p.LastName;
            p.Username = person.Username;
            p.Address = person.Address;
            p.PhoneNumber = person.PhoneNumber;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(int id, Role role)
        {
            var person = await this.GetByIdAsync(id);
            _dbContext.Users.FirstOrDefault(p => p.Id == id).Role = role;
            await _dbContext.SaveChangesAsync();            
        }
    }
}

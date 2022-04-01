using AptekaAPI.Data;
using AptekaAPI.Entitys.Enums;
using AptekaAPI.Helpers;
using AptekaAPI.Interfaces;
using AptekaAPI.JWT;
using AptekaAPI.Models;
using AptekaAPI.Models.Admin.ViewModel;
using AptekaAPI.Models.Medicine;
using AptekaAPI.Models.person.ViewModel;
using AptekaAPI.Models.Vendor.PostModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Services
{
    public class VendorService : IVendorService
    {
        private DataContext _dbContext;
        private IJwtUtils _jwtUtils;
        private AppSettings _appSettings;
        public VendorService(
            DataContext dbContext,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public async Task<VendorAuthRes> LoginAsync(LoginAuth model)
        {
            var person = await _dbContext.Users.FirstOrDefaultAsync(v => v.Username == model.Username);

            if (person is null || !CryptoPassword.Verify(model.Password, person.Password))
                throw new Exception("Login yoki Password xato!");

            var jwtToken = _jwtUtils.GenerateJwtToken(person);

            return new VendorAuthRes(person, jwtToken);
        }
        public async Task ChangePasswordAsync(string username, string phoneNumber, string oldPassword, string newPassword)
        {
            var person = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (person is null || person.PhoneNumber != phoneNumber)
                throw new Exception("username yoki telefon raqam xato!");
            if (!CryptoPassword.Verify(oldPassword, person.Password))
                throw new Exception("Avvalgi parolingiz xato");
            person.Password = CryptoPassword.Hash(newPassword);
            _dbContext.Users.Update(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task LogoutAsync()
        {

            //_context.Items["Person"] = new Person();
        }

        public async Task<IEnumerable<MedicineView>> SearchMedicineAsync(
            string searchText,
            SearchMedicineTypeEnum searchMedicineTypeEnum = SearchMedicineTypeEnum.Name)
        {
            if (searchMedicineTypeEnum == SearchMedicineTypeEnum.Name)
            {
                var medis = _dbContext.Medicines.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                if (medis is null)
                    throw new Exception("Hech qanday ma'lumot topilmadi.");
                return toMedicineViewAll(medis);
            }
            else if (searchMedicineTypeEnum == SearchMedicineTypeEnum.Country)
            {
                throw new Exception("Hozrcha tizimda faqat dori-darmon nomi bilan qidirish mavjud!");
            }
            else if (searchMedicineTypeEnum == SearchMedicineTypeEnum.Discription)
            {
                throw new Exception("Hozrcha tizimda faqat dori-darmon nomi bilan qidirish mavjud!");
            }
            else if (searchMedicineTypeEnum == SearchMedicineTypeEnum.Type)
            {
                throw new Exception("Hozrcha tizimda faqat dori-darmon nomi bilan qidirish mavjud!");
            }
            else
            {
                throw new Exception("Hozrcha tizimda faqat dori-darmon nomi, turi, izohi va mamlakati bilan qidirish mavjud!");
            }
        }

        public async Task SellMedicinesAsync(IEnumerable<SellMedicine> sellMedicines)
        {
            if (sellMedicines is null || sellMedicines.ToArray()==Array.Empty<SellMedicine>())
                throw new Exception("Ro'yhat bo'sh bo'lishi mumkin emas!");
           /* await _dbContext.Sales.AddAsync(new Sales
            {
                Narxi = await this.NarxniAniqlashAsync(sellMedicines),
                SellMedicines = (List<SellMedicine>)sellMedicines,
                SoldTime=sellMedicines?.ToList()?[0]?.SoldTime,
                VendorId= (sellMedicines.ToList()[0].VendorId)
            });*/
        }

        public async Task<decimal> NarxniAniqlashAsync(IEnumerable<SellMedicine> sellMedicines)
        {
            decimal narx = 0;
            foreach (var sm in sellMedicines)
            {
                if (sm is null)
                    throw new Exception("Malumotlar to'liq emas!");
                var medicine = await _dbContext.Medicines.FirstOrDefaultAsync(m => m.Id == sm.MedicineId);
                if (medicine is null)
                    throw new Exception($"{sm.MedicineId} - id'dagi dori bazadan topilmadi.");
                if (sm.IsBox)
                {
                    narx += medicine.BoxPrice * sm.HowMany;
                }
                else
                {
                    narx += (medicine.BoxPrice / medicine.HowMany) * sm.HowMany;
                }
            }
            return narx;
        }

        /***********************************/
        private IEnumerable<MedicineView> toMedicineViewAll(IEnumerable<Medicine> medicines)
        {
            foreach (var m in medicines.ToList())
            {
                yield return new MedicineView
                {
                    Id = m.Id,
                    MedicineType = toMedicineTypeView(_dbContext.MedicineTypes.FirstOrDefault(x => x.Id == m.MedicineTypeId)),
                    TimeOfProduction = m.TimeOfProduction,
                    ShelfLife = m.ShelfLife,
                    Name = m.Name,
                    Country = toCountryView(_dbContext.Countrys.FirstOrDefault(x => x.Id == m.CountryId)),
                    BoxPrice = m.BoxPrice,
                    HowMany = m.HowMany,
                    Discription = m.Discription
                };
            }
        }
        private MedicineTypeView toMedicineTypeView(MedicineType medicineType)
                => new MedicineTypeView
                {
                    Id = medicineType.Id,
                    Description = medicineType.Description,
                    Title = medicineType.Title
                };
        private CountryView toCountryView(Country country)
            => new CountryView
            {
                Id = country.Id,
                Name = country.Name
            };
    }
}

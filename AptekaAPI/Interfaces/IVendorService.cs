using AptekaAPI.Entitys.Enums;
using AptekaAPI.Models;
using AptekaAPI.Models.Medicine;
using AptekaAPI.Models.person.ViewModel;
using AptekaAPI.Models.Vendor.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Interfaces
{
    public interface IVendorService
    {
        Task<VendorAuthRes> LoginAsync(LoginAuth model);

        Task LogoutAsync();

        Task ChangePasswordAsync(string username, string phoneNumber, string oldPassword, string newPassword);

        Task<IEnumerable<MedicineView>> SearchMedicineAsync(string searchText,
            SearchMedicineTypeEnum searchMedicineTypeEnum = SearchMedicineTypeEnum.Name);

        /// <summary>
        /// dor-darmonlarni sotish
        /// </summary>
        /// <param name="sellMedicines"></param>
        /// <returns></returns>
        Task SellMedicinesAsync(IEnumerable<SellMedicine> sellMedicines);

        /// <summary>
        /// dorilar sotilishidan oldin narxini aniqlash
        /// </summary>
        /// <param name="sellMedicines"></param>
        /// <returns></returns>
        Task<decimal> NarxniAniqlashAsync(IEnumerable<SellMedicine> sellMedicines);
    }
}

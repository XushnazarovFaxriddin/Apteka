using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Entitys.Enums
{
    public enum Role
    {
        /// <summary>
        /// Kassir dorilarni sotishi, qidirishi va shunga o'xshash amallarni bajarishi mumkin.
        /// </summary>
        [Description("Sotuvchi")]
        Vendor = 0,


        /// <summary>
        /// Admin barcha huquqlar mavjud.
        /// </summary>
        [Description("Adminstrator")]
        Admin = 1,


        /// <summary>
        /// Admin roledan farqi o'laroq yangi admin qo'shishi yoki ularni o'chirishi mumkin.
        /// </summary>
        [Description("Super Adminstrator.")]
        SuperAdmin = 2
    }
}

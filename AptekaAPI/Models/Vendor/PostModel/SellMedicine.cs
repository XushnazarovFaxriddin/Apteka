using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptekaAPI.Models.Vendor.PostModel
{
    public class SellMedicine
    {
        /// <summary>
        /// dori-darmin id si
        /// </summary>
        public int MedicineId { get; set; }

        /// <summary>
        /// true bo'lsa pachkada false bo'lsa donada.
        /// </summary>
        public bool IsBox { get; set; }

        /// <summary>
        /// necha dona, pachka yoki dona
        /// </summary>
        public int HowMany { get; set; }

        /// <summary>
        /// sotuvchining id'si
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// dori-darmon sotilgan vaqt
        /// format: "Fri, 16 May 2015 05:50:06 GMT"
        /// </summary>
        [JsonIgnore]
        public string SoldTime = DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'");
    }
}

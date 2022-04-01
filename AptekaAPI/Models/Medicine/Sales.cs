using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AptekaAPI.Models.Admin.PostModel;
using AptekaAPI.Models.Vendor.PostModel;

namespace AptekaAPI.Models.Medicine
{
    public class Sales
    {
        /// <summary>
        /// primary key, auto inkrement
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Sotuvchi id'si
        /// </summary>
        [ForeignKey(nameof(Vendor))]
        public int VendorId { get; set; }

        public Admin.PostModel.Vendor Vendor { get; set; }

        /// <summary>
        /// sotilgan vaqt
        /// </summary>
        public string SoldTime { get; set; }

        /// <summary>
        /// Sotgan maxsulotlari
        /// </summary>
        public ICollection<SellMedicine> SellMedicines { get; set; }

        /// <summary>
        /// umumiy sotgan maxsulotlarining narxi.
        /// </summary>
        public decimal Narxi { get; set; }
    }
}

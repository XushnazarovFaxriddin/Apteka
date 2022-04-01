using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptekaAPI.Models.Medicine
{
    public class Medicine
    {
        /// <summary>
        /// Auto increment, Jsonda ko'rinmaydi.
        /// </summary>
        [Key, JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// MediceniType'ni id'si, yani qaysi turga tegishli
        /// </summary>
        [ForeignKey(nameof(Medicine))]
        public int MedicineTypeId { get; set; }

        /// <summary>
        /// Ishlab chiqarilgan vaqti
        /// </summary>
        public DateTime? TimeOfProduction { get; set; }
        
        /// <summary>
        /// Yaroqlilik muddati, qachongacha?
        /// </summary>
        public DateTime? ShelfLife { get; set; }

        /// <summary>
        /// Medicine nomi
        /// </summary>
        [Required, MinLength(3), MaxLength(60)]
        public string Name { get; set; }

        /// <summary>
        /// Mamlakat id si
        /// </summary>
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        /// <summary>
        /// bir pachka midicine'ning narxi
        /// </summary>
        [Required]
        public decimal BoxPrice { get; set; }

        /// <summary>
        /// bitta pachkada necha dona, default 1 ta.
        /// </summary>
        [Range(1,1000),DefaultValue(1)]
        public int HowMany { get; set; }

        /// <summary>
        /// Medicine haqida malumot, default null
        /// </summary>
        public string? Discription { get; set; }

    }
    public class MedicineView
    {
        public int Id { get; set; }

        public MedicineTypeView MedicineType { get; set; }

        public DateTime? TimeOfProduction { get; set; }

        public DateTime? ShelfLife { get; set; }

        public string Name { get; set; }

        public CountryView Country { get; set; }

        public decimal BoxPrice { get; set; }

        public int HowMany { get; set; }

        public string? Discription { get; set; }
    }
}

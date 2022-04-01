using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptekaAPI.Models.Medicine
{
    public class MedicineType
    {
        /// <summary>
        /// Auto increment, Jsonda ko'rinmaydi.
        /// </summary>
        [Key, JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Medicine (dori-darmon) turi yoki tur nomi
        /// </summary>
        [Required, MinLength(3), MaxLength(60, ErrorMessage = "60 ta belgidan ko'p bo'la olmaydi. Sig'masa descriptionga yozing ...")]
        public string Title { get; set; }

        // dori-darmon turi uchun izoh, default null
        public string? Description { get; set; }
    }
    public class MedicineTypeView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}

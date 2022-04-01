using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Models.Admin.PostModel
{
    public class PersonPost
    {
        /// <summary>
        /// Sotuvchining nomi
        /// </summary>
        [Required, MinLength(3), MaxLength(16)]
        public string FirstName { get; set; }

        /// <summary>
        /// Sotuvchining familyasi
        /// </summary>
        [Required, MinLength(3), MaxLength(16)]
        public string LastName { get; set; }

        /// <summary>
        /// Sotuvchinig telefon raqami
        /// standart: +(998)93 683-15-55
        /// </summary>
        [Required, RegularExpression(@"(?:\+\([9]{2}[8]\)[33||55||65||71||77||88||90||91||93||94||95||97||99]{2}\ [0-9]{3}\-[0-9]{2}\-[0-9]{2})", ErrorMessage = "Invalid Phone Number. For example:+(998)93 683-15-55")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Sotuvchining manzili
        /// Length: 10-255
        /// </summary>
        [Required, MinLength(10), MaxLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// Sotuvchining usernamesi, takrorlanmas
        /// </summary>
        [Required, MinLength(4), MaxLength(16)]
        public string Username { get; set; }

    }
}

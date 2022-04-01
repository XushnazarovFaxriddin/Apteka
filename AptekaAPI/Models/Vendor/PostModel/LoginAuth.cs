using System.ComponentModel.DataAnnotations;
namespace AptekaAPI.Models.Vendor.PostModel
{
    public class LoginAuth
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

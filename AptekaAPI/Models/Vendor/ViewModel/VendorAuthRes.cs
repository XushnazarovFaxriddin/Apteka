using AptekaAPI.Entitys.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using AptekaAPI.Models.Admin.PostModel;
using System.Threading.Tasks;

namespace AptekaAPI.Models.person.ViewModel
{
    public class VendorAuthRes
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public VendorAuthRes(Admin.PostModel.Vendor person, string token)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Username = person.Username;
            PhoneNumber = person.PhoneNumber;
            Address = person.Address;
            Role = person.Role;
            Token = token;
        }
    }
}

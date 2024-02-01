using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Data.DataModels
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        
        public Address Address { get; set; }
    }
}

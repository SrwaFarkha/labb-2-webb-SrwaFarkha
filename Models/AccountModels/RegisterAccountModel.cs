using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Models.AccountModels
{
    public class RegisterAccountModel
    {
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Display(Name = "Emailadress")]
        public string EmailAddress { get; set; }

        [Display(Name = "Lösenord")]
        public string Password { get; set; }

		[Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }

        [Display(Name = "Adress")]
        public string StreetAddress { get; set; }
    }
}

namespace Models.AccountModels
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public string City { get; set; }
        public string StreetAddress { get; set; }
        
        public AddressModel Address { get; set; }
    }
}

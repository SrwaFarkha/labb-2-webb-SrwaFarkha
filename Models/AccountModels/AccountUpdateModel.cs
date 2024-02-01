namespace Models.AccountModels
{
    public class AccountUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public AddressModel Address { get; set; }
    }
}

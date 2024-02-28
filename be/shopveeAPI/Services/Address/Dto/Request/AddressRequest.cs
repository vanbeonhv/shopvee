using shopveeAPI.Enum;


namespace shopveeAPI.Services.Address.Dto.Request
{
    public class AddressRequest
    {
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string AddressLine { get; set; }

        public bool IsDefault { get; set; }

        public Guid UserId { get; set; }

        public AddressType AddressType { get; set; } = AddressType.Default;

        //public Models.User User { get; set; }
    }
}

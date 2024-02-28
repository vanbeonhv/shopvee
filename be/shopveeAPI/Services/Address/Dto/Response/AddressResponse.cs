using shopveeAPI.Enum;

namespace shopveeAPI.Services.Address.Dto.Response
{
    public class AddressResponse
    {
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string AddressLine { get; set; }


        public AddressType AddressType { get; set; } = AddressType.Default;

    }
}

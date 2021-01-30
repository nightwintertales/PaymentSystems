namespace PaymentSystems.Domain.Accounts
{
     public record Address {
        public Address(string postcode, string streetAddress) {
            Postcode      = postcode;
            StreetAddress = streetAddress;
            Coordinates   = coordinates;
        }

        internal Address() { }

        public string         StreetAddress { get; init; }
        public string         Postcode      { get; init; }
    }

}
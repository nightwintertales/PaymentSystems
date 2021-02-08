namespace PaymentSystems.Domain.Shared {
    public static class Shared {
        public static class V1 {
            public record CounterParty(string Name, DomesticAccountDetails AccountDetails);

            public record DomesticAccountDetails(string SortCode, string AccountNumber);
        }
    }
}
using System;

namespace PaymentSystems.Domain.Accounts
{
    public record CustomerDetails {
        public CustomerDetails(string firstName, string lastName, DateTime doB) {
            FirstName      =  firstName;
            LastName = lastName;
            DoB   = doB;
        }

        internal CustomerDetails() { }

        public string   FirstName { get; init; }
        public string   LastName  { get; init; }
        public DateTime  DoB      { get; init; }
    }

}
namespace PaymentSystems.Domain.Services
{
    public class DomainServices
    {
        public delegate Task<bool> CheckAccountBalance(decimal paynentAmount);
    }
}
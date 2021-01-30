namespace PaymentSystems.Infrastructure
{
    public interface IEventHandler 
    {
        Task HandleEvent(object @event);
    }

}
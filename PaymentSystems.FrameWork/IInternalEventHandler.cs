namespace PaymentSystems.FrameWork
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }

}
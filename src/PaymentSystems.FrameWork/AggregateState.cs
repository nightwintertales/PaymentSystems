namespace PaymentSystems.FrameWork
{
    public abstract record AggregateState<TId> where TId : AggregateId {
        public int Version { get; init; } = -1;
        public TId Id { get; init; }
    }
}
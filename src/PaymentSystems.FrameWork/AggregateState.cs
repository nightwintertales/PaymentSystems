using System;
namespace PaymentSystems.FrameWork
{
    public abstract class AggregateState<TId> : Document where TId : AggregateId {
        public int Version { get; init; } = -1;
    }
}
using System.Collections.Generic;

namespace PaymentSystems.FrameWork
{
     public abstract class Aggregate<TId, TState> where TId : AggregateId where TState : AggregateState<TId> {
        public TState State { get; set; }

        readonly List<object> _changes = new();

        public IReadOnlyCollection<object> Changes => _changes.AsReadOnly();

        protected void Apply(object evt) {
            _changes.Add(evt);
            State = When(evt);
        }

        public abstract TState When(object evt);

        protected void EnsureExists() {
           // if (State.Version < 0) throw new DomainException($"{GetType().Name} {State.Id} doesn't exist");
        }

        protected void EnsureDoesntExist() {
         //   if (State.Version > -1) throw new DomainException($"{GetType().Name} {State.Id} already exist");
        }
    }

}
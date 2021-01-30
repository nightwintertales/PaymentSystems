using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentSystems.FrameWork
{
    public class CommandService<T, TId>
       where T : AggregateRoot<TId>, new()
    {
        readonly IAggregateStore _store;
        readonly Dictionary<Type, Func<T, object, Task>> _actions = new();
        readonly Dictionary<Type, Func<object, TId>> _getId = new();

        protected CommandService(IAggregateStore store) => _store = store;

        protected void OnNew<TCommand>(Func<T, TCommand, Task> action)
            => _actions.Add(typeof(TCommand), (aggregate, cmd) => action(aggregate, (TCommand)cmd));

        protected void OnNew<TCommand>(Action<T, TCommand> action)
            => _actions.Add(typeof(TCommand), (aggregate, cmd) => Sync(() => action(aggregate, (TCommand)cmd)));

        protected void OnExisting<TCommand>(Func<TCommand, TId> getId, Func<T, TCommand, Task> action)
        {
            _actions.Add(typeof(TCommand), (aggregate, cmd) => action(aggregate, (TCommand)cmd));
            _getId.Add(typeof(TCommand), cmd => getId((TCommand)cmd));
        }

        protected void OnExisting<TCommand>(Func<TCommand, TId> getId, Action<T, TCommand> action)
        {
            _actions.Add(typeof(TCommand), (aggregate, cmd) => Sync(() => action(aggregate, (TCommand)cmd)));
            _getId.Add(typeof(TCommand), cmd => getId((TCommand)cmd));
        }

        public async Task HandleNew<TCommand>(TCommand cmd, CancellationToken cancellationToken)
        {
            var aggregate = new T();
            await _actions[typeof(TCommand)](aggregate, cmd);
            await _store.Save<T,TId>(aggregate, cancellationToken);
        }

        public async Task HandleExisting<TCommand>(TCommand cmd, CancellationToken cancellationToken)
        {
            var id = _getId[typeof(TCommand)](cmd);
            var aggregate = await _store.Load<T, TId>(id, cancellationToken);
            await _actions[typeof(TCommand)](aggregate, cmd);
            await _store.Save<T, TId>(aggregate, cancellationToken);
        }

        static Task Sync(Action action)
        {
            action();
            return Task.CompletedTask;
        }
    }

}
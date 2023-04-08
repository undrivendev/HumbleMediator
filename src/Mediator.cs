using System;

namespace HumbleMediator;

public sealed class Mediator : IMediator
{
    private readonly Func<Type, object?> _serviceFactory;

    public Mediator(Func<Type, object?> serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<TQueryResult> SendQuery<TQuery, TQueryResult>(
        TQuery query,
        CancellationToken cancellationToken = default
    )
        where TQuery : IQuery<TQueryResult> =>
        await GetService<IQueryHandler<TQuery, TQueryResult>>().Handle(query, cancellationToken);

    public async Task<TCommandResult> SendCommand<TCommand, TCommandResult>(
        TCommand command,
        CancellationToken cancellationToken = default
    )
        where TCommand : ICommand<TCommandResult> =>
        await GetService<ICommandHandler<TCommand, TCommandResult>>()
            .Handle(command, cancellationToken);

    private T GetService<T>()
    {
        var type = typeof(T);
        return (T)(
            _serviceFactory(type)
            ?? throw new InvalidOperationException($"Service not found for type: {type}")
        );
    }
}

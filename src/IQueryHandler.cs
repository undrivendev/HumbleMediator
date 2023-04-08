namespace HumbleMediator;

public interface IQueryHandler<in TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
{
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellationToken = default);
}

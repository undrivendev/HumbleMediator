namespace HumbleMediator.DependencyInjection;

public interface IContainer
{
    public TService Resolve<TService>() where TService : notnull;
}
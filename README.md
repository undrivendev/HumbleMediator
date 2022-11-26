# HumbleMediator
[![Build Status](https://dev.azure.com/undrivendev/HumbleMediator/_apis/build/status/undrivendev.HumbleMediator?branchName=main)](https://dev.azure.com/undrivendev/HumbleMediator/_build/latest?definitionId=3&branchName=main)
[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/undrivendev/HumbleMediator/3/main)](https://dev.azure.com/undrivendev/HumbleMediator/_build/latest?definitionId=3&branchName=main)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=undrivendev_HumbleMediator&metric=alert_status)](https://sonarcloud.io/summary/overall?id=undrivendev_HumbleMediator)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=undrivendev_HumbleMediator&metric=coverage)](https://sonarcloud.io/summary/overall?id=undrivendev_HumbleMediator)

HumbleMediator is a simple library (~50 lines of code) containing the minimum amount of abstractions/boilerplate code to implement a functioning Mediator pattern implementation for CQRS.

It is designed to leverage the functionality provided by a Dependency Injection container, so if you're not using one in your project, this is not for you.

## Like it? Give a star! :star:
If you like this project, you learned something from it or you are using it in your applications, please press the star button. Thanks!

## Installation
```
dotnet add package HumbleMediator
```

## Registration
Register the `IMediator` interface with the Dependency Injection container of your choice by creating an instance of the `Mediator` class and pass a `Func<Type, object?>` as a constructor argument.

This delegate should point to the service resolution method of the Dependency Injection container itself.

Some examples:

### ASP.NET Core
```
services.AddSingleton<IMediator>(sp => new Mediator(sp.GetService));
```

### SimpleInjector
```
container.Register<IMediator>(() => new Mediator(container.GetInstance), Lifestyle.Singleton);
```
### Generic IServiceProvider
```
container.Register<IMediator>(() => new Mediator((container as IServiceProvider).GetService));
```
## Usage
Create the necessary queries and/or commands by marking the DTOs with the appropriate `ICommand<TResult>` or `IQuery<TResult>` interface.
```
public record CreateCustomerCommand(Customer Customer) : ICommand<int>;
```

Create a handler for each of those commands and/or queries by creating the necessary handlers implementing the `ICommandHandler<TCommand, TResult>` or `IQueryHandler<TQuery, TResult>` interfaces.
```
public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, int>
{
    public async Task<int> Handle(
        CreateCustomerCommand command,
        CancellationToken cancellationToken = default
    )
    {
        // Handler logic
    }
}
```
Call the handlers via the `IMediator` interface.
```
public class CustomersController
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create()
    {
        return await _mediator.SendCommand<CreateCustomerCommand, int>(
            new CreateCustomerCommand(new Customer())
        );
    }
}
```

## Cross-cutting concerns
It's possible to implement [cross-cutting concerns](https://en.wikipedia.org/wiki/Cross-cutting_concern) by leveraging the [Decorator](https://en.wikipedia.org/wiki/Decorator_pattern) pattern on the `IMediator` interface.

By doing that, you'll be able to extend the default behaviour with some custom logic that will be executed every time the `IMediator` interface is called.

Some examples of cross-cutting concerns could be:
- Logging
- Validation
- Caching

and so on.

## Samples
A working example in the context of an ASP.NET Web API, including an implementation of cross-cutting concerns, can be found in my [other project](https://github.com/undrivendev/template-webapi-aspnet).

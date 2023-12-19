using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HumbleMediator.Tests.Stubs;
using NSubstitute;
using Xunit;

namespace HumbleMediator.Tests.Mediator;

public class SendCommand
{
    [Fact]
    public async Task ShouldResolveAndHandle()
    {
        // Arrange
        const int expectedResult = 67;
        var command = new CommandStub();
        var subHandler = Substitute.For<ICommandHandler<CommandStub, int>>();
        subHandler.Handle(command).Returns(expectedResult);

        var sut = new HumbleMediator.Mediator(_ => subHandler);

        // Act
        var result = await sut.SendCommand<CommandStub, int>(command);

        // Assert
        await subHandler.Received(1).Handle(command);
        result.Should().Be(expectedResult);
    }

    [Fact]
    public async Task WhenUnableToResolveShouldThrowWithMessageIncludingTypeName()
    {
        // Arrange
        var sut = new HumbleMediator.Mediator(_ => null);

        // Act
        var act = () => sut.SendCommand<CommandStub, int>(new CommandStub());

        // Assert
        await act.Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .Where(e => e.Message.EndsWith(typeof(ICommandHandler<CommandStub, int>).ToString()));
    }
}

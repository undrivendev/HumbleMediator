using System.Threading.Tasks;
using FluentAssertions;
using HumbleMediator.DependencyInjection;
using HumbleMediator.Tests.Stubs;
using Moq;
using Xunit;

namespace HumbleMediator.Tests;

public class MediatorTests
{
    [Fact]
    public async Task SendQuery_ShouldResolveAndHandle()
    {
        // Arrange
        var expectedResult = 66;
        var query = new QueryStub();
        var mockHandler = new Mock<IQueryHandler<QueryStub, int>>();
        mockHandler.Setup(e => e.Handle(query, default)).ReturnsAsync(expectedResult);
        
        var container = new Mock<IContainer>();
        container.Setup(e => e.Resolve<IQueryHandler<QueryStub, int>>())
            .Returns(mockHandler.Object);

        var sut = new Mediator(container.Object);

        // Act
        var result = await sut.SendQuery<QueryStub, int>(query);

        // Assert
        container.Verify(e => e.Resolve<IQueryHandler<QueryStub, int>>(), Times.Once);
        mockHandler.Verify(e => e.Handle(It.Is<QueryStub>(f => f == query), default), Times.Once);
        result.Should().Be(expectedResult);
    }

    [Fact]
    public async Task SendCommand_ShouldResolveAndHandle()
    {
        // Arrange
        var expectedResult = 67;
        var command = new CommandStub();
        var mockHandler = new Mock<ICommandHandler<CommandStub, int>>();
        mockHandler.Setup(e => e.Handle(command, default)).ReturnsAsync(expectedResult);
        
        var container = new Mock<IContainer>();
        container.Setup(e => e.Resolve<ICommandHandler<CommandStub, int>>())
            .Returns(mockHandler.Object);

        var sut = new Mediator(container.Object);

        // Act
        var result = await sut.SendCommand<CommandStub, int>(command);

        // Assert
        container.Verify(e => e.Resolve<ICommandHandler<CommandStub, int>>(), Times.Once);
        mockHandler.Verify(e => e.Handle(It.Is<CommandStub>(f => f == command), default), Times.Once);
        result.Should().Be(expectedResult);
    }
}
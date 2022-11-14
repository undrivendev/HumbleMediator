using System;
using System.Threading.Tasks;
using FluentAssertions;
using HumbleMediator.Tests.Stubs;
using Moq;
using Xunit;

namespace HumbleMediator.Tests.Mediator;

public class SendQuery
{
    [Fact]
    public async Task ShouldResolveAndHandle()
    {
        // Arrange
        const int expectedResult = 66;
        var query = new QueryStub();
        var mockHandler = new Mock<IQueryHandler<QueryStub, int>>();
        mockHandler.Setup(e => e.Handle(query, default)).ReturnsAsync(expectedResult);

        var sut = new HumbleMediator.Mediator(_ => mockHandler.Object);

        // Act
        var result = await sut.SendQuery<QueryStub, int>(query);

        // Assert
        mockHandler.Verify(e => e.Handle(It.Is<QueryStub>(f => f == query), default), Times.Once);
        result.Should().Be(expectedResult);
    }

    [Fact]
    public async Task WhenUnableToResolveShouldThrowWithMessageIncludingTypeName()
    {
        // Arrange
        var sut = new HumbleMediator.Mediator(_ => null);

        // Act
        var act = () => sut.SendQuery<QueryStub, int>(new QueryStub());

        // Assert
        await act.Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .Where(e => e.Message.EndsWith(typeof(IQueryHandler<QueryStub, int>).ToString()));
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using HumbleMediator.Tests.Stubs;
using NSubstitute;
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
        var subHandler = Substitute.For<IQueryHandler<QueryStub, int>>();
        subHandler.Handle(query).Returns(expectedResult);

        var sut = new HumbleMediator.Mediator(_ => subHandler);

        // Act
        var result = await sut.SendQuery<QueryStub, int>(query);

        // Assert
        await subHandler.Received(1).Handle(query);
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

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sisteplant.Application.Queries.Exercise1;

namespace Sisteplant.UnitTests.Exercises
{
    public class Exercise1UnitTest
    {
        private readonly IMediator _mediator;

        public Exercise1UnitTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Exercise1QueryHandler>())
                .BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }
        [Fact]
        public async Task Exercise1Async()
        {
            // Assert
            Assert.Equal(5268, await _mediator.Send(new Exercise1Query(268)));
            Assert.Equal(6750, await _mediator.Send(new Exercise1Query(670)));
            Assert.Equal(50, await _mediator.Send(new Exercise1Query(0)));
            Assert.Equal(-5999, await _mediator.Send(new Exercise1Query(-999)));
            // Test cases for out of range
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await _mediator.Send(new Exercise1Query(-8001)));

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await _mediator.Send(new Exercise1Query(8001)));
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sisteplant.Application.Queries.Exercise3;
using Sisteplant.Infrastructure.Exercises;

namespace Sisteplant.UnitTests.Exercises.Exercise3
{
    public class Exercise3UnitTest : Exercise3DBTestBase
    {
        private readonly IMediator _mediator;

        public Exercise3UnitTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Exercise3QueryHandler>())
                .AddDbContext<SisteplantContext>(options =>
                    options.UseInMemoryDatabase("SisteplantTestDb")) // DB context in DI
                .BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async void TestReturning1Distributor()
        {
            // Arrange: Populate the database to return one distributor
            var context = CreateInMemoryDatabase();
            SeedDatabase(context, "Returning1Distributor");

            // Act: Execute the query
            var result = await _mediator.Send(new Exercise3Query());

            // Assert: Validate the result contains the expected distributor ID and only one result
            Assert.Contains(result, distributorId => distributorId == 503);
            Assert.Single(result);
        }

        [Fact]
        public async void TestReturning2Distributors()
        {
            // Arrange: Populate the database to return two distributors
            var context = CreateInMemoryDatabase();
            SeedDatabase(context, "Returning2Distributors");

            // Act: Execute the query
            var result = await _mediator.Send(new Exercise3Query());

            // Assert: Validate that multiple distributors are returned
            Assert.Contains(result, distributorId => distributorId == 501);
            Assert.Contains(result, distributorId => distributorId == 503);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void TestReturningEmptyList()
        {
            // Arrange: Populate the database to return an empty distributor list
            var context = CreateInMemoryDatabase();
            SeedDatabase(context, "ReturningEmptyList");

            // Act: Execute the query
            var result = await _mediator.Send(new Exercise3Query());

            // Assert: Validate that no distributors are returned
            Assert.Empty(result);
        }
    }
}

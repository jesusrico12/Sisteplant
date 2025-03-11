using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sisteplant.Application.Query.Exercise2;

namespace Sisteplant.UnitTests.Exercises
{
    public class Exercise2UnitTest
    {
        private readonly IMediator _mediator;

        public Exercise2UnitTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Exercise2QueryHandler>())
                .BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Exercise2Async()
        {
            // Case 1: Normal sequence
            int[] input1 = { 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 };
            Assert.Equal(1, await _mediator.Send(new Exercise2Query(input1))); // The first maximum sequence of 1s starts at index 1

            // Case 2: No 1s
            int[] input2 = { 0, 0, 0, 0 };
            Assert.Equal(-1, await _mediator.Send(new Exercise2Query(input2))); // No sequences of 1s, expected -1

            // Case 3: Maximum length sequence at the beginning
            int[] input3 = { 1, 1, 1, 0, 0, 1 };
            Assert.Equal(0, await _mediator.Send(new Exercise2Query(input3))); // The first maximum sequence of 1s starts at index 0

            // Case 4: First long sequence in the middle
            int[] input4 = { 0, 1, 1, 1, 1, 0, 1 };
            Assert.Equal(1, await _mediator.Send(new Exercise2Query(input4))); // The first maximum sequence of 1s starts at index 1

            // Case 5: All 1s
            int[] input5 = { 1, 1, 1, 1 };
            Assert.Equal(0, await _mediator.Send(new Exercise2Query(input5))); // The first maximum sequence of 1s starts at index 0

            // Case 6: Multiple sequences of the same length
            int[] input6 = { 1, 1, 0, 1, 1, 0 };
            Assert.Equal(0, await _mediator.Send(new Exercise2Query(input6))); // The first sequence (length 2) starts at index 0

            // Case 7: Sequences of 1s in a single-element array
            int[] input7 = { 1 };
            Assert.Equal(0, await _mediator.Send(new Exercise2Query(input7))); // The sequence of 1s starts at index 0

            // Case 8: Empty array of 0s
            int[] input8 = { 0 };
            Assert.Equal(-1, await _mediator.Send(new Exercise2Query(input8))); // No sequences of 1s, expected -1
        }
    }
}

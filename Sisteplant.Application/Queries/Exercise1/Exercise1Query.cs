using MediatR;

namespace Sisteplant.Application.Queries.Exercise1
{
    public class Exercise1Query : IRequest<int>
    {
        public int Number { get; }

        public Exercise1Query(int number)
        {
            Number = number;
        }

    }
}

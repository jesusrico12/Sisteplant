using MediatR;

namespace Sisteplant.Application.Query.Exercise2
{
    public class Exercise2Query : IRequest<int>
    {
        public int[] ArrayA { get; }

        public Exercise2Query(int[] arrayA)
        {
            ArrayA = arrayA;
        }
    }
}

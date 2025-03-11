using MediatR;
using Microsoft.EntityFrameworkCore;
using Sisteplant.Infrastructure.Exercises;

namespace Sisteplant.Application.Queries.Exercise3
{
    /// <summary>
    /// Handles the queries related to exercise 3,
    /// implementing <see cref="IRequestHandler{TRequest, TResponse}"/> 
    /// to process <see cref="Exercise3Query"/> and return a list of distributor IDs.
    /// </summary>
    public class Exercise3QueryHandler : IRequestHandler<Exercise3Query, List<int>>
    {
        private readonly SisteplantContext _context;

        public Exercise3QueryHandler(SisteplantContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the execution of exercise 3 query, 
        /// retrieving the IDs of distributors who have purchased all types of items 
        /// from the company.
        /// </summary>
        /// <param name="request">The query to process.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>A list of distributor IDs.</returns>
        public async Task<List<int>> Handle(Exercise3Query request, CancellationToken cancellationToken)
        {
            /*
             * // Original SQL query that was replaced with a LINQ approach.
             * 
                string sql = @"
                SELECT o.distributor_id
                FROM orders o
                GROUP BY o.distributor_id
                HAVING COUNT(DISTINCT o.item_ordered) = (SELECT COUNT(*) FROM items);
                ";
            */

            var distributorIds = await _context.Orders
                .GroupBy(o => o.DistributorId)
                .Where(g => g.Select(o => o.ItemOrdered).Distinct().Count() == _context.Items.Count())
                .Select(g => g.Key)
                .ToListAsync(cancellationToken);

            return distributorIds;
        }
    }
}

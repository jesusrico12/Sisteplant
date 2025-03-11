using MediatR;

namespace Sisteplant.Application.Queries.Exercise1
{
    /// <summary>
    /// Handles the execution of the Exercise1 query to determine the maximum possible value
    /// by inserting the digit '5' into the integer representation of the input number.
    /// This code is written using C# 12
    /// </summary>
    public class Exercise1QueryHandler : IRequestHandler<Exercise1Query, int>
    {
        /// <summary>
        /// Processes the <see cref="Exercise1Query"/> request to insert a '5'
        /// into the integer in such a way that the absolute value of the result is maximized.
        /// </summary>
        /// <param name="request">The query request containing the integer number.</param>
        /// <param name="cancellationToken">A cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, with the result as an integer.</returns>
        
        public Task<int> Handle(Exercise1Query request, CancellationToken cancellationToken)
        {

            // Validate the range of the number
            if ((request.Number < -8000 || request.Number> 8000))
            {
                throw new ArgumentOutOfRangeException(nameof(request.Number),
                    $"The number must be in the range of -8000 to 8000.");
            }

            string numStr = request.Number.ToString();
            bool isNegative = request.Number < 0;
            char toInsert = '5';

            for (int i = (isNegative ? 1 : 0); i < numStr.Length; i++)
            {
                // If the number is positive, start inserting from the beginning of the string (index 0).
                // If the number is negative, skip the first character (the '-' sign) by starting from index 1.

                // Check if the number is positive
                if (!isNegative && numStr[i] < toInsert)
                {
                    // If the current digit is less than '5', inserting '5' here will produce a larger number,
                    // so perform the insertion and return the new integer value.
                    return Task.FromResult(int.Parse(numStr.Insert(i, toInsert.ToString())));
                }

                // Check if the number is negative
                else if (isNegative && numStr[i] > toInsert)
                {
                    // If the current digit is greater than '5', inserting '5' here will make the number
                    // less negative (closer to zero), so perform the insertion and return the new integer value.
                    return Task.FromResult(int.Parse(numStr.Insert(i, toInsert.ToString())));
                }
            }

            // If no suitable position was found in the loop, append '5' to the end of the string representation.
            return Task.FromResult(int.Parse(numStr + toInsert));
        }
    }
}

using MediatR;

namespace Sisteplant.Application.Query.Exercise2
{
    /// <summary>
    /// Represents a solution to find the starting position of the first sequence of consecutive 1s
    /// in a binary array that has the maximum length of consecutive 1s.
    /// </summary>
    public class Exercise2QueryHandler : IRequestHandler<Exercise2Query, int>
    {
        /// <summary>
        /// Finds the starting index of the first sequence of consecutive 1s that is the longest in the array.
        /// If there are no 1s in the array, returns -1.
        /// Assumptions:
        /// N is an integer in the range [1..1000];
        /// Each element of array A is an integer that can have one of the following values 0:1
        /// </summary>
        /// <param name="A">An array of integers (0s and 1s).</param>
        /// <returns>
        /// The starting position of the longest consecutive sequence of 1s, or -1 if no 1s are present.
        /// </returns>
        public Task<int> Handle(Exercise2Query request, CancellationToken cancellationToken)
        {
            int n = request.ArrayA.Length; // Array length
            int i = n - 1;                 // Start iterating from the last index
            int result = -1;               // Default result if no sequence of 1s is found
            int k = 0, maximal = 0;        // `k` counts current consecutive 1s, `maximal` stores the longest sequence length

            while (i >= 0) // Start from the end of the array and iterate backwards, ensuring `i == 0` is included (line modified)
            {
                if (request.ArrayA[i] == 1) // If the current element is 1
                {
                    k = k + 1; // Increment consecutive 1s counter
                    if (k >= maximal) // If this sequence is the longest so far
                    {
                        maximal = k;  // Update the maximal length
                        result = i;   // Update the result to the start index of this sequence
                    }
                }
                else
                
                    k = 0; // Reset the counter if a 0 is encountered
                
                i = i - 1; // Move to the previous element
            }

            if (request.ArrayA[0] == 1 && k + 1 > maximal) //It can be evaluated if the first element of the Array is 1 and if the sequence of 1's is greater than the maximum.
                result = 0;
            return Task.FromResult(result); // Return the result (index of the first 1 in the longest sequence)
        }
    }
}

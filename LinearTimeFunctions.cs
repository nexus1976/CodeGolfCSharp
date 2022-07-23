using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSPractice
{
    /// <summary>
    /// This class contains functions that have O(n) time complexity
    /// </summary>
    public class LinearTimeFunctions
    {

        /// <summary>
        /// Given a presumed-unsorted array of floats, find all pairs in said array whose sum is less than or equal to a float input value.
        /// </summary>
        /// <param name="inputValue">The input float value that all found float pair sums should be less than or equal to.</param>
        /// <param name="arr">The input array of float values to find the pairs from.</param>
        /// <returns>A list of float tuples where each tuple is the pair of floats from the input array whose sum is less than or equal to the input value.</returns>
        public static List<(float, float)> GetPairsWhoseSumIsLessThanOrEqualToInputValue(float inputValue, float[] arr)
        {
            throw new NotImplementedException();
        }
    }
}

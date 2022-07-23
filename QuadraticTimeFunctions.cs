using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSPractice
{
    /// <summary>
    /// This class contains functions that have O(n*n) time complexity
    /// </summary>
    public class QuadraticTimeFunctions
    {
        /// <summary>
        /// Given a presumed-unsorted array of floats, find all pairs in said array whose sum is less than or equal to a float input value.
        /// </summary>
        /// <param name="inputValue">The input float value that all found float pair sums should be less than or equal to.</param>
        /// <param name="arr">The input array of float values to find the pairs from.</param>
        /// <returns>A list of float tuples where each tuple is the pair of floats from the input array whose sum is less than or equal to the input value.</returns>
        public static List<(float, float)> GetPairsWhoseSumIsLessThanOrEqualToInputValue(float inputValue, float[] arr)
        {
            List<(float, float)> result = new();
            if (inputValue == 0 || arr.Length == 0)
                return result;

            for (int i = 0; i < arr.Length; i++)
            {
                float val1 = arr[i];
                for (int x = 0; x < arr.Length; x++)
                {
                    if (i != x)
                    {
                        float val2 = arr[x];
                        if (val1 + val2 <= inputValue)
                        {
                            var sortedTyple = GetSortedTuple((val1, val2));
                            if (!result.Contains(sortedTyple))
                                result.Add(sortedTyple);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Given a presumed-unsorted array of numbers, find all pais in said array whose sum is less than or equal to an input number value.
        /// </summary>
        /// <typeparam name="T">The generic number type</typeparam>
        /// <param name="inputValue">The input number value that all found number pair sums should be less than or equal to.</param>
        /// <param name="arr">The input array of number values to find the pairs from.</param>
        /// <returns>A list of number tuples where each tuple is the pair of numbers from the input array whose sum is less than or equal to the input value.</returns>
        public static List<(T, T)> GetPairsWhoseSumIsLessThanOrEqualToInputValue<T>(T inputValue, T[] arr)  where T : IComparable, IConvertible
        {
            List<(T, T)> result = new();
            var type = typeof(T);
            if (!IsNumberType(type))
                return result;
            if (IsZero(inputValue) || arr.Length == 0)
                return result;

            for (int i = 0; i < arr.Length; i++)
            {
                T val1 = arr[i];
                for (int x = 0; x < arr.Length; x++)
                {
                    if (i != x)
                    {
                        T val2 = arr[x];
                        var sum = Sum((val1, val2));
                        if (sum.CompareTo(inputValue) <= 0)
                        {
                            var sortedTyple = GetSortedTuple((val1, val2));
                            if (!result.Contains(sortedTyple))
                                result.Add(sortedTyple);
                        }
                    }
                }
            }

            return result;
        }

        private static bool IsZero<T>(T val) where T: IComparable, IConvertible
        {
            var type = typeof(T);
            if (type == typeof(short)) return val.ToInt16(null) == 0;
            if (type == typeof(int)) return val.ToInt32(null) == 0;
            if (type == typeof(long)) return val.ToInt64(null) == 0;
            if (type == typeof(double)) return val.ToDouble(null) == 0;
            if (type == typeof(float)) return val.ToSingle(null) == 0;
            if (type == typeof(decimal)) return val.ToDecimal(null) == 0;
            
            throw new ArgumentException("Unsupported Type");
        }

        private static bool IsNumberType(Type type)
        {
            if (type == null) return false;
            if (type == typeof(short)) return true;
            if (type == typeof(int)) return true;
            if (type == typeof(long)) return true;
            if (type == typeof(double)) return true;
            if (type == typeof(float)) return true;
            if (type == typeof(decimal)) return true;
            return false;
        }

        private static (T, T) GetSortedTuple<T>((T val1, T val2) inputValue) where T : IComparable
        {
            var type = typeof(T);
            if (!IsNumberType(type))
                return inputValue;
            if (inputValue.val1.CompareTo(inputValue.val2) <= 0)
                return inputValue;
            else
                return (inputValue.val2, inputValue.val1);
        }

        private static T Sum<T> ((T val1, T val2) inputValue) where T : IComparable, IConvertible
        {
            var type = typeof(T);
            if (!IsNumberType(type))
                throw new ArgumentException("Unsupported Type");
            T result = (T)(0 as IConvertible).ToType(type, null);
            if (type == typeof(short))
            {
                var sum = inputValue.val1.ToInt16(null) + inputValue.val2.ToInt16(null);
                if (sum > short.MaxValue)
                    throw new OverflowException("The sum value exceeds the maximum value for type Int16");
                result = (T)(sum as IConvertible).ToType(type, null);
            }
            if (type == typeof(int))
            {
                var sum = inputValue.val1.ToInt32(null) + inputValue.val2.ToInt32(null);
                if (sum > int.MaxValue)
                    throw new OverflowException("The sum value exceeds the maximum value for type Int32");
                result = (T)(sum as IConvertible).ToType(type, null);
            }
            if (type == typeof(long))
            {
                var sum = inputValue.val1.ToInt64(null) + inputValue.val2.ToInt64(null);
                if (sum > long.MaxValue)
                    throw new OverflowException("The sum value exceeds the maximum value for type Int64");
                result = (T)(sum as IConvertible).ToType(type, null);
            }
            if (type == typeof(double))
            {
                var sum = inputValue.val1.ToDouble(null) + inputValue.val2.ToDouble(null);
                if (sum > double.MaxValue)
                    throw new OverflowException("The sum value exceeds the maximum value for type Double");
                result = (T)(sum as IConvertible).ToType(type, null);
            }
            if (type == typeof(float))
            {
                var sum = inputValue.val1.ToSingle(null) + inputValue.val2.ToSingle(null);
                if (sum > float.MaxValue)
                    throw new OverflowException("The sum value exceeds the maximum value for type Float / Single");
                result = (T)(sum as IConvertible).ToType(type, null);
            }
            if (type == typeof(decimal))
            {
                var sum = inputValue.val1.ToDecimal(null) + inputValue.val2.ToDecimal(null);
                if (sum > decimal.MaxValue)
                    throw new OverflowException("The sum value exceeds the maximum value for type Decimal");
                result = (T)(sum as IConvertible).ToType(type, null);
            }

            return result;
        }
    }
}

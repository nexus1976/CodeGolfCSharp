namespace AWSPractice.Test
{
    [TestClass]
    public class TestQuadraticTimeFunctions
    {
        [TestMethod]
        public void WhenUsingFloats_GetPairsWhoseSumIsLessThanOrEqualToInputValue_MultiplePairs()
        {
            // Arrange
            List<float> floats = new()
            {
                4.5f, 8.5f, 1.5f, 0.5f, 12.5f, 9.5f, 5.5f
            };
            float inputValue = 6f;

            // Act
            var result = QuadraticTimeFunctions.GetPairsWhoseSumIsLessThanOrEqualToInputValue(inputValue, floats.ToArray());

            // Assert
            // The pairs from the floats array whose sum is less than or equal to 6 are:
            // (0.5, 4.5), (0.5, 5.5), (0.5, 1.5), (1.5, 4.5)
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 4);
            Assert.IsTrue(result.Contains((0.5f, 4.5f)));
            Assert.IsTrue(result.Contains((0.5f, 5.5f)));
            Assert.IsTrue(result.Contains((0.5f, 1.5f)));
            Assert.IsTrue(result.Contains((1.5f, 4.5f)));
        }

        [TestMethod]
        public void WhenUsingGenerics_Longs_GetParisWhoseSumIsLessThanOrEqualToInputValue_MultiplePairs()
        {
            // Arrange
            List<long> longs = new()
            {
                4, 8, 9, 6, 5, 1, 3, 2
            };
            long inputValue = 10;

            // Act
            var result = QuadraticTimeFunctions.GetPairsWhoseSumIsLessThanOrEqualToInputValue(inputValue, longs.ToArray());

            // Assert
            // THe pairs from the long array whose sum is less than or equal to 10 are:
            // (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 8), (1, 9)
            // (2, 3), (2, 4), (2, 5), (2, 6), (2, 8)
            // (3, 4), (3, 5), (3, 6)
            // (4, 5), (4, 6)
            // 17 tuples total
            Assert.IsTrue(result.Count == 17);
        }
    }
}
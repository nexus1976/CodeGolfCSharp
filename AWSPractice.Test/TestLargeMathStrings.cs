using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSPractice.Test
{
    [TestClass]
    public class TestLargeMathStrings
    {
        [TestMethod]
        public void WhenAddingLargeNumbersAsStrings_Positive_NoDecimals()
        {
            // Arrange
            string largeNumber1 = "9,223,372,036,854,775,807";
            string largeNumber2 = "99";
            string expectedResult = "9223372036854775906";

            // Act
            string result = LargeMathStrings.PerformLargeAdditionString(largeNumber1, largeNumber2);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void WhenAddingLargeNumbersAsStrings_Negative_NoDecimals()
        {
            // Arrange
            string largeNumber1 = "-9,223,372,036,854,775,807";
            string largeNumber2 = "-99";
            string expectedResult = "-9223372036854775906";

            // Act
            string result = LargeMathStrings.PerformLargeAdditionString(largeNumber1, largeNumber2);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void WhenAddingLargeNumbersAsStrings_Positive_Decimals()
        {
            // Arrange
            string largeNumber1 = "9,223,372,036,854,775,807.9123";
            string largeNumber2 = "99.6";
            string expectedResult = "9223372036854775907.5123";

            // Act
            string result = LargeMathStrings.PerformLargeAdditionString(largeNumber1, largeNumber2);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }
    }
}

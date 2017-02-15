using System;
using Xunit;

namespace Flirsty.Utilities.Test
{
    public class AgeCalculatorTests
    {
        [Fact]
        public void Calculate_Should_Return_8_When_BirthDate_Is_2000_02_29_And_Today_Is_2009_02_28()
        {
            // Arrange
            DateTime bDay = new DateTime(2000, 2, 29);
            DateTime now = new DateTime(2009, 2, 28);

            // Act
            int age = AgeCalculator.Calculate(now, bDay);

            // Assert
            Assert.Equal(8, age);
        }
    }
}
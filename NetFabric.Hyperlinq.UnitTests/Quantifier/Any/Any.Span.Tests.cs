using System;
using System.Collections.Generic;
using System.Diagnostics;
using NetFabric.Assertive;
using Xunit;

namespace NetFabric.Hyperlinq.UnitTests
{
    public class AnySpanTests
    {
        [Fact]
        public void Select_With_NullPredicate_Should_Throw()
        {
            // Arrange
            var predicate = (Func<int, bool>)null;

            // Act
            Action action = () => SpanExtensions
                .Any<int>(new int[0], predicate);

            // Assert
            action.Must()
                .Throw<ArgumentNullException>()
                .EvaluatesTrue(exception => exception.ParamName == "predicate");
        }

        [Theory]
        [MemberData(nameof(TestData.Any), MemberType = typeof(TestData))]
        public void Any_With_ValidData_Should_Succeed(int[] source, Func<int, bool> predicate)
        {
            // Arrange
            var expected = 
                System.Linq.Enumerable.Any(source, predicate);

            // Act
            var result = SpanExtensions
                .Any<int>(source, predicate);

            // Assert
            result.Must()
                .BeEqualTo(expected);
        }
    }
}
using NetFabric.Assertive;
using System;
using Xunit;

namespace NetFabric.Hyperlinq.UnitTests
{
    public class ElementAtValueReadOnlyListTests
    {
        [Theory]
        [MemberData(nameof(TestData.ElementAtOutOfRange), MemberType = typeof(TestData))]
        public void ElementAt_With_OutOfRange_Should_Throw(int[] source, int index)
        {
            // Arrange
            var wrapped = Wrap
                .AsValueReadOnlyList(source);

            // Act
            Action action = () => ValueReadOnlyList
                .ElementAt<Wrap.ValueReadOnlyList<int>, Wrap.Enumerator<int>, int>(wrapped, index);

            // Assert
            action.Must()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [MemberData(nameof(TestData.ElementAt), MemberType = typeof(TestData))]
        public void ElementAt_With_ValidData_Should_Succeed(int[] source, int index)
        {
            // Arrange
            var wrapped = Wrap
                .AsValueReadOnlyList(source);
            var expected = 
                System.Linq.Enumerable.ElementAt(wrapped, index);

            // Act
            var result = ValueReadOnlyList
                .ElementAt<Wrap.ValueReadOnlyList<int>, Wrap.Enumerator<int>, int>(wrapped, index);

            // Assert
            result.Must()
                .BeEqualTo(expected);
        }
    }
}
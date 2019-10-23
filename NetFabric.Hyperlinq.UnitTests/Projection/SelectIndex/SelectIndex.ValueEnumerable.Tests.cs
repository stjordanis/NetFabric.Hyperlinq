using NetFabric.Assertive;
using System;
using Xunit;

namespace NetFabric.Hyperlinq.UnitTests
{
    public class SelectIndexValueEnumerableTests
    {
        [Fact]
        public void SelectIndex_With_NullSelector_Should_Throw()
        {
            // Arrange
            var enumerable = Wrap.AsValueEnumerable(new int[0]);
            var selector = (Func<int, string>)null;

            // Act
            Action action = () => ValueEnumerable.Select<Wrap.ValueEnumerable<int>, Wrap.Enumerator<int>, int, string>(enumerable, selector);

            // Assert
            action.Must()
                .Throw<ArgumentNullException>()
                .EvaluatesTrue(exception => exception.ParamName == "selector");
        }

        [Theory]
        [MemberData(nameof(TestData.Empty), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.Single), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.Multiple), MemberType = typeof(TestData))]
        public void SelectIndex_With_ValidData_Should_Succeed(int[] source)
        {
            // Arrange
            var wrapped = Wrap.AsValueEnumerable(source);
            var expected = 
                System.Linq.Enumerable.Select(wrapped, (item, index) => (item + index).ToString());

            // Act
            var result = ValueEnumerable
                .Select<Wrap.ValueEnumerable<int>, Wrap.Enumerator<int>, int, string>(wrapped, (item, index) => (item + index).ToString());

            // Assert
            result.Must()
                .BeEnumerableOf<string>()
                .BeEqualTo(expected);
        }
    }
}
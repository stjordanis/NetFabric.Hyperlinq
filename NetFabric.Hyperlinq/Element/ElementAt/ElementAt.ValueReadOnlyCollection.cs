using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace NetFabric.Hyperlinq
{
    public static partial class ValueReadOnlyCollection
    {
        [Pure]
        public static TSource ElementAt<TEnumerable, TEnumerator, TSource>(this TEnumerable source, int index)
            where TEnumerable : IValueReadOnlyCollection<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
        {
            if (index >= 0 && index < source.Count)
            {
                using var enumerator = source.GetEnumerator();
                for (; enumerator.MoveNext(); index--)
                {
                    if (index == 0)
                        return enumerator.Current;
                }
            }

            return ThrowHelper.ThrowArgumentOutOfRangeException<TSource>(nameof(index));
        }

        [Pure]
        public static TSource ElementAtOrDefault<TEnumerable, TEnumerator, TSource>(this TEnumerable source, int index)
            where TEnumerable : IValueReadOnlyCollection<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
        {
            if (index >= 0 && index < source.Count)
            {
                using var enumerator = source.GetEnumerator();
                for (; enumerator.MoveNext(); index--)
                {
                    if (index == 0)
                        return enumerator.Current;
                }
            }

            return default;
        }

        [Pure]
        public static Maybe<TSource> TryElementAt<TEnumerable, TEnumerator, TSource>(this TEnumerable source, int index)
            where TEnumerable : IValueReadOnlyCollection<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
        {
            if (index >= 0 && index < source.Count)
            {
                using var enumerator = source.GetEnumerator();
                for (; enumerator.MoveNext(); index--)
                {
                    if (index == 0)
                        return new Maybe<TSource>(enumerator.Current);
                }
            }

            return default;
        }
    }
}
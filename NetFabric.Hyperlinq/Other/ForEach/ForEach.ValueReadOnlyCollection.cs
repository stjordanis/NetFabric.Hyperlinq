﻿using System;
using System.Collections.Generic;

namespace NetFabric.Hyperlinq
{
    public static partial class ValueReadOnlyCollection
    {
        public static void ForEach<TEnumerable, TEnumerator, TSource>(this TEnumerable source, Action<TSource> action)
            where TEnumerable : IValueReadOnlyCollection<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
        {
            if (action is null) ThrowHelper.ThrowArgumentNullException(nameof(action));

            if (source.Count != 0)
            {
                using var enumerator = source.GetEnumerator();
                while (enumerator.MoveNext())
                    action(enumerator.Current);
            }
        }

        public static void ForEach<TEnumerable, TEnumerator, TSource>(this TEnumerable source, Action<TSource, int> action)
            where TEnumerable : IValueReadOnlyCollection<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
        {
            if (action is null) ThrowHelper.ThrowArgumentNullException(nameof(action));

            if (source.Count != 0)
            {
                using var enumerator = source.GetEnumerator();
                checked
                {
                    for (var index = 0; enumerator.MoveNext(); index++)
                        action(enumerator.Current, index);
                }
            }
        }
    }
}

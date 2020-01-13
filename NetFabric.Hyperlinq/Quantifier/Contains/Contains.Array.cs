using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class Array
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<TSource>(this TSource[] source, TSource value)
            => System.Array.IndexOf<TSource>(source, value) >= 0;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool Contains<TSource>(this TSource[] source, TSource value, int skipCount, int takeCount)
            => System.Array.IndexOf<TSource>(source, value, skipCount, takeCount) >= 0;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<TSource>(this TSource[] source, TSource value, IEqualityComparer<TSource>? comparer)
            => Contains<TSource>(source, value, comparer, 0, source.Length);

        [Pure]
        static bool Contains<TSource>(this TSource[] source, TSource value, IEqualityComparer<TSource>? comparer, int skipCount, int takeCount)
        {
            if (takeCount != 0)
            {
                if (comparer is null)
                    return System.Array.IndexOf<TSource>(source, value, skipCount, takeCount) >= 0;

                var end = skipCount + takeCount;
                for (var index = skipCount; index < end; index++)
                {
                    if (comparer.Equals(value, source[index]))
                        return true;
                }
            }
            return false;
        }
    }
}


using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class ReadOnlySpanExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly TSource First<TSource>(this ReadOnlySpan<TSource> source)
        {
            if (source.Length == 0) ThrowHelper.ThrowEmptySequence<TSource>();

            return ref source[0];
        }

        [Pure]
        public static ref readonly TSource First<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, bool> predicate)
        {
            if (predicate is null) ThrowHelper.ThrowArgumentNullException(nameof(predicate));

            var length = source.Length;
            for (var index = 0; index < length; index++)
            {
                if (predicate(source[index]))
                    return ref source[index];
            }
            ThrowHelper.ThrowEmptySequence<TSource>();
            return ref source[0];
        }

        [Pure]
        public static ref readonly TSource First<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, long, bool> predicate)
        {
            if (predicate is null) ThrowHelper.ThrowArgumentNullException(nameof(predicate));

            var length = source.Length;
            for (var index = 0; index < length; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            ThrowHelper.ThrowEmptySequence<TSource>();
            return ref source[0];
        }

        [Pure]
        public static ref readonly TSource First<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, long, bool> predicate, out int index)
        {
            if (predicate is null) ThrowHelper.ThrowArgumentNullException(nameof(predicate));

            var length = source.Length;
            for (index = 0; index < length; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            ThrowHelper.ThrowEmptySequence<TSource>();
            return ref source[0];
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly TSource FirstOrDefault<TSource>(this ReadOnlySpan<TSource> source)
        {
            if (source.Length == 0) return ref Default<TSource>.Value;

            return ref source[0];
        }

        [Pure]
        public static ref readonly TSource FirstOrDefault<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, bool> predicate)
        {
            if (predicate is null) ThrowHelper.ThrowArgumentNullException(nameof(predicate));

            var length = source.Length;
            for (var index = 0; index < length; index++)
            {
                if (predicate(source[index]))
                    return ref source[index];
            }
            return ref Default<TSource>.Value;
        }

        [Pure]
        public static ref readonly TSource FirstOrDefault<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, long, bool> predicate)
        {
            if (predicate is null) ThrowHelper.ThrowArgumentNullException(nameof(predicate));

            var length = source.Length;
            for (var index = 0; index < length; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            return ref Default<TSource>.Value;
        }

        [Pure]
        public static ref readonly TSource FirstOrDefault<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, long, bool> predicate, out int index)
        {
            if (predicate is null) ThrowHelper.ThrowArgumentNullException(nameof(predicate));

            var length = source.Length;
            for (index = 0; index < length; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            index = -1;
            return ref Default<TSource>.Value;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSource? FirstOrNull<TSource>(this ReadOnlySpan<TSource> source)
            where TSource : struct
        {
            if (source.Length == 0) return null;

            return source[0];
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSource? FirstOrNull<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, long, bool> predicate)
            where TSource : struct
            => FirstOrNull<TSource>(source, predicate, out var _);

        [Pure]
        public static TSource? FirstOrNull<TSource>(this ReadOnlySpan<TSource> source, Func<TSource, long, bool> predicate, out int index)
            where TSource : struct
        {
            if (predicate is null) ThrowHelper.ThrowArgumentNullException(nameof(predicate));

            var length = source.Length;
            for (index = 0; index < length; index++)
            {
                if (predicate(source[index], index))
                    return source[index];
            }
            index = -1;
            return null;
        }
    }
}

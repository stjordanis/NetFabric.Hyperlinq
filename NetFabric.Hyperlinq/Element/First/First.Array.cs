using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class Array
    {
        [Pure]
        public static ref readonly TSource First<TSource>(this TSource[] source)
        {
            if (source.Length == 0) Throw.EmptySequence<TSource>();

            return ref source[0];
        }

        [Pure]
        static ref readonly TSource First<TSource>(this TSource[] source, int skipCount, int takeCount)
        {
            if (takeCount == 0) Throw.EmptySequence<TSource>();

            return ref source[skipCount];
        }

        [Pure]
        public static ref readonly TSource First<TSource>(this TSource[] source, Predicate<TSource> predicate) 
        {
            if (predicate is null) Throw.ArgumentNullException(nameof(predicate));

            return ref First<TSource>(source, predicate, 0, source.Length);
        }

        [Pure]
        static ref readonly TSource First<TSource>(this TSource[] source, Predicate<TSource> predicate, int skipCount, int takeCount)
        {
            var end = skipCount + takeCount;
            for (var index = skipCount; index < end; index++)
            {
                if (predicate(source[index]))
                    return ref source[index];
            }
            return ref Throw.EmptySequenceRef<TSource>();
        }

        [Pure]
        public static ref readonly TSource First<TSource>(this TSource[] source, PredicateAt<TSource> predicate)
        {
            if (predicate is null) Throw.ArgumentNullException(nameof(predicate));

            return ref First<TSource>(source, predicate, 0, source.Length);
        }

        [Pure]
        static ref readonly TSource First<TSource>(this TSource[] source, PredicateAt<TSource> predicate, int skipCount, int takeCount)
        {
            var end = skipCount + takeCount;
            for (var index = skipCount; index < end; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            return ref Throw.EmptySequenceRef<TSource>();
        }

        [Pure]
        public static ref readonly TSource First<TSource>(this TSource[] source, PredicateAt<TSource> predicate, out int index) 
        {
            if (predicate is null) Throw.ArgumentNullException(nameof(predicate));

            return ref First<TSource>(source, predicate, out index, 0, source.Length);
        }

        [Pure]
        static ref readonly TSource First<TSource>(this TSource[] source, PredicateAt<TSource> predicate, out int index, int skipCount, int takeCount)
        {
            var end = skipCount + takeCount;
            for (index = skipCount; index < end; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            index = -1;
            return ref Throw.EmptySequenceRef<TSource>();
        }

        [Pure]
        [return: MaybeNull]
        public static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source)
        {
            if (source.Length == 0) return ref Default<TSource>.Value;

            return ref source[0];
        }

        [Pure]
        [return: MaybeNull]
        static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source, int skipCount, int takeCount)
        {
            if (takeCount == 0) return ref Default<TSource>.Value;

            return ref source[skipCount];
        }

        [Pure]
        [return: MaybeNull]
        public static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source, Predicate<TSource> predicate) 
        {
            if (predicate is null) Throw.ArgumentNullException(nameof(predicate));

            return ref FirstOrDefault<TSource>(source, predicate, 0, source.Length);
        }

        [Pure]
        [return: MaybeNull]
        static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source, Predicate<TSource> predicate, int skipCount, int takeCount)
        {
            var end = skipCount + takeCount;
            for (var index = skipCount; index < end; index++)
            {
                if (predicate(source[index]))
                    return ref source[index];
            }
            return ref Default<TSource>.Value;
        }

        [Pure]
        [return: MaybeNull]
        public static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source, PredicateAt<TSource> predicate)
        {
            if (predicate is null) Throw.ArgumentNullException(nameof(predicate));

            return ref FirstOrDefault<TSource>(source, predicate, 0, source.Length);
        }

        [Pure]
        [return: MaybeNull]
        static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source, PredicateAt<TSource> predicate, int skipCount, int takeCount)
        {
            var end = skipCount + takeCount;
            for (var index = skipCount; index < end; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            return ref Default<TSource>.Value;
        }

        [Pure]
        [return: MaybeNull]
        public static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source, PredicateAt<TSource> predicate, out int index) 
        {
            if (predicate is null) Throw.ArgumentNullException(nameof(predicate));

            return ref FirstOrDefault<TSource>(source, predicate, out index, 0, source.Length);
        }

        [Pure]
        [return: MaybeNull]
        static ref readonly TSource FirstOrDefault<TSource>(this TSource[] source, PredicateAt<TSource> predicate, out int index, int skipCount, int takeCount)
        {
            var end = skipCount + takeCount;
            for (index = skipCount; index < end; index++)
            {
                if (predicate(source[index], index))
                    return ref source[index];
            }
            index = -1;
            return ref Default<TSource>.Value;
        }
    }
}

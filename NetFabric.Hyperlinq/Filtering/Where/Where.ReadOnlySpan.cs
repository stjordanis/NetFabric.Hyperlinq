﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class SpanExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RefWhereEnumerable<TSource> Where<TSource>(this ReadOnlySpan<TSource> source, Predicate<TSource> predicate) 
        {
            if (predicate is null) Throw.ArgumentNullException(nameof(predicate));

            return new RefWhereEnumerable<TSource>(source, predicate);
        }

        public readonly ref struct RefWhereEnumerable<TSource>
        {
            internal readonly ReadOnlySpan<TSource> source;
            internal readonly Predicate<TSource> predicate;

            internal RefWhereEnumerable(in ReadOnlySpan<TSource> source, Predicate<TSource> predicate)
            {
                this.source = source;
                this.predicate = predicate;
            }
            
            [Pure]
            public readonly Enumerator GetEnumerator() => new Enumerator(in this);

            public ref struct Enumerator 
            {
                readonly ReadOnlySpan<TSource> source;
                readonly Predicate<TSource> predicate;
                int index;

                internal Enumerator(in RefWhereEnumerable<TSource> enumerable)
                {
                    source = enumerable.source;
                    predicate = enumerable.predicate;
                    index = -1;
                }

                [MaybeNull]
                public readonly ref readonly TSource Current 
                    => ref source[index];

                public bool MoveNext()
                {
                    while (++index < source.Length)
                    {
                        if (predicate(source[index]))
                            return true;
                    }
                    return false;
                }
            }

            public int Count()
                => source.Count(predicate);

            public RefWhereSelectEnumerable<TSource, TResult> Select<TResult>(Selector<TSource, TResult> selector)
                => WhereSelect<TSource, TResult>(source, predicate, selector);

            public TSource First()
                => source.First(predicate);

            [return: MaybeNull]
            public TSource FirstOrDefault()
                => source.FirstOrDefault(predicate);

            public TSource Single()
                => source.Single(predicate);

            [return: MaybeNull]
            public TSource SingleOrDefault()
                => source.SingleOrDefault(predicate);

            public void ForEach(Action<TSource> action)
                => source.ForEach(action, predicate);
            public void ForEach(Action<TSource, int> action)
                => source.ForEach(action, predicate);
        }
    }
}

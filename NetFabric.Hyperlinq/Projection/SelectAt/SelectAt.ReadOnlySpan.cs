﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class ArrayExtensions
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanSelectAtEnumerable<TSource, TResult> Select<TSource, TResult>(this ReadOnlySpan<TSource> source, NullableSelectorAt<TSource, TResult> selector)
        {
            if (selector is null) Throw.ArgumentNullException(nameof(selector));

            return new SpanSelectAtEnumerable<TSource, TResult>(source, selector);
        }

        [GeneratorMapping("TSource", "TResult")]
        public readonly ref struct SpanSelectAtEnumerable<TSource, TResult>
        {
            internal readonly ReadOnlySpan<TSource> source;
            internal readonly NullableSelectorAt<TSource, TResult> selector;

            internal SpanSelectAtEnumerable(ReadOnlySpan<TSource> source, NullableSelectorAt<TSource, TResult> selector)
            {
                this.source = source;
                this.selector = selector;
            }

            
            public readonly Enumerator GetEnumerator() 
                => new Enumerator(in this);

            public readonly int Count 
                => source.Length;

            [MaybeNull]
            public readonly TResult this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => selector(source[index], index);
            }

            public ref struct Enumerator
            {
                readonly ReadOnlySpan<TSource> source;
                readonly NullableSelectorAt<TSource, TResult> selector;
                readonly int end;
                int index;

                internal Enumerator(in SpanSelectAtEnumerable<TSource, TResult> enumerable)
                {
                    source = enumerable.source;
                    selector = enumerable.selector;
                    index = -1;
                    end = index + source.Length;
                }

                [MaybeNull]
                public readonly TResult Current 
                    => selector(source[index], index);

                public bool MoveNext() 
                    => ++index <= end;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Any()
                => source.Length != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> ElementAt(int index)
                => ArrayExtensions.ElementAt<TSource, TResult>(source, index, selector);

            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> First()
                => ArrayExtensions.First<TSource, TResult>(source, selector);

            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> Single()
                => ArrayExtensions.Single<TSource, TResult>(source, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TResult[] ToArray()
                => ArrayExtensions.ToArray(source, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IMemoryOwner<TResult> ToArray(MemoryPool<TResult> pool)
                => ArrayExtensions.ToArray(source, selector, pool);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public List<TResult> ToList()
                => ArrayExtensions.ToList(source, selector);

            public bool SequenceEqual(IEnumerable<TResult> other, IEqualityComparer<TResult>? comparer = null)
            {
                comparer ??= EqualityComparer<TResult>.Default;

                var enumerator = GetEnumerator();
                using var otherEnumerator = other.GetEnumerator();
                while (true)
                {
                    var thisEnded = !enumerator.MoveNext();
                    var otherEnded = !otherEnumerator.MoveNext();

                    if (thisEnded != otherEnded)
                        return false;

                    if (thisEnded)
                        return true;

                    if (!comparer.Equals(enumerator.Current!, otherEnumerator.Current))
                        return false;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Count<TSource, TResult>(this SpanSelectAtEnumerable<TSource, TResult> source)
            => source.Count;
    }
}


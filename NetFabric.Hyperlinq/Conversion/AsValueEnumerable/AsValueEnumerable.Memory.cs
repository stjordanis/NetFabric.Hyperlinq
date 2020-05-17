﻿using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class Array
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemoryValueEnumerableWrapper<TSource> AsValueEnumerable<TSource>(this Memory<TSource> source)
            => AsValueEnumerable((ReadOnlyMemory<TSource>)source);
    }
}
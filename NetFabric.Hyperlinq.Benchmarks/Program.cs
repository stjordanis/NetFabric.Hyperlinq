﻿using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace NetFabric.Hyperlinq.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[] {
                typeof(IndexerBenchmarks),
                typeof(VirtualCallBenchmarks),
                typeof(ValueEnumerableBenchmarks),
                typeof(CastBenchmarks),
                typeof(GenerationOperationsBenchmarks),
                typeof(EmptyBenchmarks),
                typeof(CountBenchmarks),
                typeof(CountPredicateBenchmarks),
                typeof(AllBenchmarks),
                typeof(AnyBenchmarks),
                typeof(ContainsBenchmarks),
                typeof(ContainsComparerBenchmarks),
                typeof(FirstBenchmarks),
                typeof(FirstOrDefaultBenchmarks),
                typeof(SingleBenchmarks),
                typeof(SingleOrDefaultBenchmarks),
                typeof(ToArrayBenchmarks),
                typeof(ToListBenchmarks),
                typeof(SelectBenchmarks),
                typeof(SelectManyBenchmarks),
                typeof(SelectCountBenchmarks),
                typeof(SelectToArrayBenchmarks),
                typeof(SelectToListBenchmarks),
                typeof(WhereBenchmarks),
                typeof(WhereCountBenchmarks),
                typeof(WhereFirstBenchmarks),
                typeof(WhereFirstOrDefaultBenchmarks),
                typeof(WhereSingleBenchmarks),
                typeof(WhereSingleOrDefaultBenchmarks),
                typeof(WhereToArrayBenchmarks),
                typeof(WhereToListBenchmarks),
                typeof(WhereSelectBenchmarks),
                typeof(WhereSelectCountBenchmarks),
            });
            switcher.Run(args);        
        }
    }
}

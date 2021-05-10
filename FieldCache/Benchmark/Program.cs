using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FieldCacheNamespace;
using System;
using System.Runtime.CompilerServices;

namespace Benchmark
{
    public static class Funcs
    {
        public static long DumbAlgLcm(long a, long b)
        {
            var min = Math.Min(a, b);
            var max = Math.Max(a, b);
            var curr = max;
            while (curr > min)
                curr -= 10000;
            return curr + max;
        }

        public static readonly long A = 138125424;
        public static readonly long B = 10341473;
    }

    public record WorksWithLazyLcm
    {
        public long Lcm => lcm.Value;
        private Lazy<long> lcm;

        public long a { get; private set; }
        public long b { get; private set; }

        public WorksWithLazyLcm(long a, long b)
        {
            this.a = a;
            this.b = b;
            lcm = new(() => Funcs.DumbAlgLcm(this.a, this.b), isThreadSafe: true);
        }
    }

    public record WorksWithFieldCacheStaticLambdaLcm(long a, long b)
    {
        public long Lcm => lcm.GetValue(static @this => Funcs.DumbAlgLcm(@this.a, @this.b), this);
        private FieldCache<long> lcm;
    }

    public record WorksWithFieldCacheStaticLambdaCachedLcm(long a, long b)
    {
        private static Func<WorksWithFieldCacheStaticLambdaCachedLcm, long> lambda = static @this => Funcs.DumbAlgLcm(@this.a, @this.b);
        public long Lcm => lcm.GetValue(lambda, this);
        private FieldCache<long> lcm;
    }

    public record WorksWithFieldCacheStaticLambdaCachedLocallyLcm(long a, long b)
    {
        public long Lcm => lcm.GetValue(localLambda, this);
        private FieldCache<long> lcm;
        private Func<WorksWithFieldCacheStaticLambdaCachedLocallyLcm, long> localLambda = lambda;
        private static Func<WorksWithFieldCacheStaticLambdaCachedLocallyLcm, long> lambda = static @this => Funcs.DumbAlgLcm(@this.a, @this.b);
    }

    public record WorksWithConditionalWeakTableLambdaLcm(long a, long b)
    {
        public long Lcm => lcm.GetValue(this, static @this => new Wrapper<long>(Funcs.DumbAlgLcm(@this.a, @this.b)));
        private readonly ConditionalWeakTable<WorksWithConditionalWeakTableLambdaLcm, Wrapper<long>> lcm = new();
    }

    public sealed record Wrapper<T>(T Value)
    {
        public static implicit operator T(Wrapper<T> wrapper) => wrapper.Value;
    }

    public class ContainerPerformance
    {
        private WorksWithLazyLcm withLazy = new(Funcs.A, Funcs.B);
        private WorksWithFieldCacheStaticLambdaLcm withStaticCache = new(Funcs.A, Funcs.B);
        private WorksWithFieldCacheStaticLambdaCachedLcm withStaticCacheLambdaCached = new(Funcs.A, Funcs.B);
        private WorksWithFieldCacheStaticLambdaCachedLocallyLcm withStaticCacheLambdaCachedLocally = new(Funcs.A, Funcs.B);
        private WorksWithConditionalWeakTableLambdaLcm worksWithConditionalWeakTableLambdaLcm = new(Funcs.A, Funcs.B);

        [Benchmark] public void BenchFunction() => Funcs.DumbAlgLcm(Funcs.A, Funcs.B);

        [Benchmark]
        public void LazyT()
        {
            var v = withLazy.Lcm;
        }

        [Benchmark]
        public void FieldCacheT()
        {
            var v = withStaticCache.Lcm;
        }

        [Benchmark]
        public void FieldCacheLambdaCachedT()
        {
            var v = withStaticCacheLambdaCached.Lcm;
        }

        [Benchmark]
        public void FieldCacheLambdaCachedLocallyT()
        {
            var v = withStaticCacheLambdaCachedLocally.Lcm;
        }

        [Benchmark]
        public void ConditionalWeakTableT()
        {
            var v = worksWithConditionalWeakTableLambdaLcm.Lcm;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ContainerPerformance>();
            //var withCache = new WorksWithFieldCacheLcm(Funcs.A, Funcs.B);
            //for (long i = 0; i < 10_000_000_000_0L; i++)
            //{
            //    var v = withCache.Lcm;
            //}
        }
    }
}

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FieldCacheNamespace;
using System;

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
            lcm = new(() => Funcs.DumbAlgLcm(this.a, this.b));
        }
    }

    public record WorksWithFieldCacheLcm
    {
        public long Lcm => lcm.GetValue;
        private FieldCache<long> lcm;

        public long a { get; private set; }
        public long b { get; private set; }

        public WorksWithFieldCacheLcm(long a, long b)
        {
            this.a = a;
            this.b = b;
            lcm = new(() => Funcs.DumbAlgLcm(this.a, this.b));
        }
    }

    public class ContainerPerformance
    {
        private WorksWithLazyLcm withLazy = new(Funcs.A, Funcs.B);
        private WorksWithFieldCacheLcm withCache = new(Funcs.A, Funcs.B);

        [Benchmark] public void BenchFunction() => Funcs.DumbAlgLcm(Funcs.A, Funcs.B);

        [Benchmark]
        public void LazyT()
        {
            var v = withLazy.Lcm;
        }

        [Benchmark]
        public void FieldCacheT()
        {
            var v = withCache.Lcm;
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

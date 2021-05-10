using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FieldCacheNamespace;
using System;
using System.Runtime.CompilerServices;

// Some constants
public static class Funcs
{
    public const int OPERATION_COUNT = 0x1000; // must be divisible by 8 so that loop unrolling works 100% correctly
    public const int ARRAY_SIZE = 100;

    public static long DumbAlg(long a, long b) => (a + b) % ARRAY_SIZE;
}



// This is the first benched method
public record WorksWithLazyRandom
{
    public long Value => value.Value;
    private Lazy<long> value;

    public long a { get; private set; }
    public long b { get; private set; }

    public WorksWithLazyRandom(long a, long b)
    {
        this.a = a;
        this.b = b;
        value = new(() => Funcs.DumbAlg(this.a, this.b), isThreadSafe: true);
    }
}


// This is the second benched method
public record WorksWithFieldCacheStaticLambdaRandom(long a, long b)
{
    public long Value => lcm.GetValue(this);
    private FieldCache<WorksWithFieldCacheStaticLambdaRandom, long> lcm = new(static @this => Funcs.DumbAlg(@this.a, @this.b));
}


// This is the third benched method
public record WorksWithConditionalWeakTableLambdaRandom(long a, long b)
{
    public long Value => lcm.GetValue(this, static @this => new Wrapper<long>(Funcs.DumbAlg(@this.a, @this.b)));
    private readonly ConditionalWeakTable<WorksWithConditionalWeakTableLambdaRandom, Wrapper<long>> lcm = new();

    public sealed record Wrapper<T>(T Value)
    {
        public static implicit operator T(Wrapper<T> wrapper) => wrapper.Value;
    }
}

// This is the benchmark
public class ContainerPerformance
{
    private WorksWithLazyRandom[] withLazy;
    private WorksWithFieldCacheStaticLambdaRandom[] withStaticCache;
    private WorksWithConditionalWeakTableLambdaRandom[] worksWithConditionalWeakTableLambda;
    private long[] justLongs;


    [GlobalSetup]
    public void Setup()
    {
        withLazy = InitArray((a, b) => new WorksWithLazyRandom(a, b));
        withStaticCache = InitArray((a, b) => new WorksWithFieldCacheStaticLambdaRandom(a, b));
        worksWithConditionalWeakTableLambda = InitArray((a, b) => new WorksWithConditionalWeakTableLambdaRandom(a, b));
        justLongs = InitArray((a, b) => (a + b) % Funcs.ARRAY_SIZE);

        // The array is designed the way that you can jump from point i-th to point array[i] and then to
        // array[array[i]] and etc. It allows to ensure that nothing is optimized out while the entire
        // array is easily fitted into L1.
        static T[] InitArray<T>(Func<long, long, T> init)
        {
            var res = new T[Funcs.ARRAY_SIZE];
            var random = new Random(165_345);
            for (int i = 0; i < Funcs.ARRAY_SIZE; i++)
                res[i] = init(random.Next(), random.Next());
            return res;
        }
    }

    [Benchmark(OperationsPerInvoke = Funcs.OPERATION_COUNT, Description = "This overhead is a tradeoff to perform a microbenchmark")]
    public long BenchmarkOverhead()
    {
        var a = 0L;
        for (int i = 0; i < Funcs.OPERATION_COUNT; i += 8)
        {
            a = justLongs[a]; a = justLongs[a];
            a = justLongs[a]; a = justLongs[a];
            a = justLongs[a]; a = justLongs[a];
            a = justLongs[a]; a = justLongs[a];
        }
        return a;
    }

    [Benchmark(OperationsPerInvoke = Funcs.OPERATION_COUNT, Description = "The time needed for standard Lazy<> to return a value", Baseline = true)]
    public long LazyT()
    {
        var a = 0L;
        for (int i = 0; i < Funcs.OPERATION_COUNT; i += 8)
        {
            a = withLazy[a].Value; a = withLazy[a].Value;
            a = withLazy[a].Value; a = withLazy[a].Value;
            a = withLazy[a].Value; a = withLazy[a].Value;
            a = withLazy[a].Value; a = withLazy[a].Value;
        }
        return a;
    }
    
    [Benchmark(OperationsPerInvoke = Funcs.OPERATION_COUNT, Description = "The time needed for FieldCache<,> to return a value")]
    public long FieldCacheT()
    {
        var a = 0L;
        for (int i = 0; i < Funcs.OPERATION_COUNT; i += 8)
        {
            a = withStaticCache[a].Value; a = withStaticCache[a].Value;
            a = withStaticCache[a].Value; a = withStaticCache[a].Value;
            a = withStaticCache[a].Value; a = withStaticCache[a].Value;
            a = withStaticCache[a].Value; a = withStaticCache[a].Value;
        }
        return a;
    }

    [Benchmark(OperationsPerInvoke = Funcs.OPERATION_COUNT, Description = "The time needed for ConditionalWeakTable<,> to return a value")]
    public long ConditionalWeakTableT()
    {
        var a = 0L;
        for (int i = 0; i < Funcs.OPERATION_COUNT; i += 8)
        {
            a = worksWithConditionalWeakTableLambda[a].Value; a = worksWithConditionalWeakTableLambda[a].Value;
            a = worksWithConditionalWeakTableLambda[a].Value; a = worksWithConditionalWeakTableLambda[a].Value;
            a = worksWithConditionalWeakTableLambda[a].Value; a = worksWithConditionalWeakTableLambda[a].Value;
            a = worksWithConditionalWeakTableLambda[a].Value; a = worksWithConditionalWeakTableLambda[a].Value;
        }
        return a;
    }
}

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<ContainerPerformance>();
    }
}
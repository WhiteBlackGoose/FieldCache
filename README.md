## FieldCache

A good replacement for the `Lazy<T>` class. Why? Because

1. It is a struct.
2. It is safe for records' `Equals` and `GetHashCode`


### Usage

Consider an immutable record `Person`, which has `FirstName` and `LastName`. It also has a property
`FullName` which should be computed once (assume it's too expensive otherwise). Then, that is how
you use `FieldCache`:
```cs
public record Person(string FirstName, string LastName)
{
	public string FullName => fullName.GetValue(@this => @this.FirstName + " " + @this.LastName, this);
	private FieldCache<string> fullName;
}
```

Since Roslyn overrides records' `Equals`, unless you use `FieldCache`, you either need to dynamically attach your fields,
or override `Equals` for all records which have cached fields.

### Benchmarks

|                   Method |          Mean |      Error |     StdDev |
|------------------------- |--------------:|-----------:|-----------:|
|            BenchFunction | 4,599.1638 ns | 90.6775 ns | 80.3832 ns |
|                  Lazy<T> |     0.6717 ns |  0.0469 ns |  0.0501 ns |
|            FieldCache<T> |     3.6674 ns |  0.0846 ns |  0.0750 ns |
| ConditionalWeakTable<,T> |    25.0521 ns |  0.4320 ns |  0.6056 ns |

(needs improvement for sure XD)
## FieldCache

A good replacement for the `Lazy<T>` class. Why? Because

1. It is a struct.
2. It is safe for records' `Equals` and `GetHashCode`


### Usage

Slow option (because capturing causes reallocation):
```cs
public record Person(string FirstName, string LastName)
{
	public string FullName => fullName.GetValue(() => FirstName + " " + LastName);
	private FieldCache<string> fullName;
}
```

Fast option (recommended):
```cs
public record Person(string FirstName, string LastName)
{
	public string FullName => fullName.GetValue(@this => @this.FirstName + " " + @this.SecondName, this);
	private FieldCache<string> fullName;
}
```

Since Roslyn overrides records' `Equals`, unless you use `FieldCache`, you either need to dynamically attach your fields,
or override `Equals` for all records which have cached fields.

### Benchmarks

|            Method |          Mean |
|------------------ |--------------:|
|     BenchFunction | 4,609.6243 ns |
|             LazyT |     0.7128 ns |
|       FieldCacheT |    16.1614 ns |
| FieldStaticCacheT |     5.1907 ns |

(needs improvement for sure XD)
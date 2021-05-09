## FieldCache

A good replacement for the `Lazy<T>` class for some rare cases. If you already hate it, go to [FAQ](#FAQ). You can also read an [article](https://habr.com/en/post/545936/) about it.

## Usage

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

## FAQ

#### 1. Why not use `Lazy<T>`?
> For many reasons. 
> 1. It takes the factory in its constructor. See question 3 why it's bad.
> 2. The comparison would be invalid, since `Lazy` are compared by references, which will always differ. See question 2.
> 3. Makes an allocation we don't need in our case.

#### 2. Why are `Equals` and `GetHashCode` overrided to true and 0?
> The values obtained by `FieldCache`'s factory must be *secondary* properties of the record. For example, for `MyInteger` the primary property is `int Value`. One of its secondary properties might be `MyInteger Tripled` defined as `Tripled => tripled.GetValue(@this => new MyInteger(@this.Value * 3), this)`. You can see, the comparison remains absoltely valid, since you don't want to compare by their secondary properties, only by those primary.

#### 3. Why do we pass the factory into `GetValue`, not in the ctor?
> Because if you needed to pass it in the constructor, you would need to have a record's constructor, where you would be creating all those `FieldCache` for each field. This is not a good code, poorly maintainable and non-obvious. Your initialization will be separated from the properties to which your values are exposed. While here this delegate is passed right in the secondary property.

#### 4. Why do we depend on the "holder"?
> Because we want to recreate the cache if the owner changed. It is useful for the `with` operator, which *copies* all the fields of a record and assign them all to a new one. To avoid rewriting member copy method, we can simply invalidate it automatically.

## Benchmarks

|                   Method |          Mean |      Error |     StdDev |
|------------------------- |--------------:|-----------:|-----------:|
|            BenchFunction | 4,599.1638 ns | 90.6775 ns | 80.3832 ns |
|                  Lazy<T> |     0.6717 ns |  0.0469 ns |  0.0501 ns |
|            FieldCache<T> |     3.6674 ns |  0.0846 ns |  0.0750 ns |
| ConditionalWeakTable<,T> |    25.0521 ns |  0.4320 ns |  0.6056 ns |

(needs improvement for sure XD)

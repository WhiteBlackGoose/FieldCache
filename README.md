## FieldCache

A good replacement for the `Lazy<T>` class. Why? Because

1. It is a struct.
2. It is safe for records' `Equals` and `GetHashCode`


### Usage

```cs
public record Person(string FirstName, string LastName)
{
	public string FullName => fullName.GetValue(() => FirstName + " " + LastName);
	private FieldCache<string> fullName;
}
```

Since Roslyn overrides records' `Equals`, unless you use `FieldCache`, you either need to dynamically attach your fields,
or override `Equals` for all records which have cached fields.
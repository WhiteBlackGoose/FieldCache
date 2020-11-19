using System;
using FieldCacheNamespace;

var personA = new Person("John", "Smith");
var personB = new Person("John", "Smith");
Console.WriteLine(personA.FullName);
Console.WriteLine(personA == personB);
Console.WriteLine(personB.FullName);
Console.WriteLine(personA == personB);

public sealed record Person(string FirstName, string SecondName)
{
    public string FullName => fullName.GetValue(
        (first, second) => first + " " + second, FirstName, SecondName);
    private FieldCache<string> fullName;
}

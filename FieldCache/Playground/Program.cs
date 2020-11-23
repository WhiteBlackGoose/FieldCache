using System;
using FieldCacheNamespace;

var personA = new Person("John", "Smith");
Console.WriteLine(personA.FullName);
var personB = personA with { FirstName = "Goose" };
Console.WriteLine(personB.FullName);



public sealed record Person(string FirstName, string SecondName)
{
    public string FullName => fullName.GetValue(
        @this => @this.FirstName + " " + @this.SecondName, this);
    private FieldCache<string> fullName;
}

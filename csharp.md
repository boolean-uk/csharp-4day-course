**Int**

- 7 / 2 = 3 (int)

**String**

- '' = char, "" = string
- prefix string with @ to avoid escaping backslash, etc (@"C:\\Users") (except for ""hello"")
- interpolation with $ ($"Hello {name}")
- string methods like ToUpper() does not modify original string (creates new string)
- .Length .Split .Join .Contains etc
- string.Equals() string.IsNullOrWhiteSpace()
- int.Parse() int.TryParse()

String formatting:

- :C currency (culture-aware: £ in en-GB, $ in en-US)
- :N2 number with 2 decimal places and thousands separators
- :F2 fixed-point, 2 decimal places, no thousands separator
- :D5 integer zero-padded to 5 digits
- :P1 percentage with 1 decimal place

**Arrays**

- arrays are fixed while lists are dynamic (size)
- defaults:

&#x09;int → 0

&#x09;double → 0.0

&#x09;bool → false

&#x09;string → null (reference types default to null)

- use for loop when you need index otherwise foreach

&#x09;or: foreach (string name in names.Select((value, index) => new { value, index }))

- Array.Sort() Array.Reverse() Array.IndexOf()

**Switch**

- have to use break/return/throw
- possible to stack case labels

**List**

- .Count

**OOP**

- struct vs class:

&#x09;struct creates deep copy when assigned or passed to function (fix by returning new copy in function)

&#x09;class is single shared instance (default to classes)

- protected: not accessible outside (object) the class/inherited classes
- always try to keep logic to private variables and keep public variables small, and only expose what you need (so we don't have to change every implementation when refactoring public variable)
- abstract class: cannot create object of class
- virtual:
- use structs for small data, e.g. coordinates, money, date ranges
  &#x09;use classes when data gets more complex than that
- polymorphism: overriding parent methods

**Enums**

- increasing int under the hood
- ToString() returns name not int

**Exceptions**

- try/catch
- use specific built-in csharp error (Exception for all, avoid)

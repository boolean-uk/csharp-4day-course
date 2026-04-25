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
- strings are immutable, new copy is created when concatenating
- StringBuilder() is mutable (faster O(n) than string concatenation O(n^2)) (prefer string.Join())

String formatting:

- :C currency (culture-aware: £ in en-GB, $ in en-US)
- :N2 number with 2 decimal places and thousands separators
- :F2 fixed-point, 2 decimal places, no thousands separator
- :D5 integer zero-padded to 5 digits
- :P1 percentage with 1 decimal place

**Arrays**

- arrays are fixed while lists are dynamic (size)
- defaults:
  - int → 0
  - double → 0.0
  - bool → false
  - string → null (reference types default to null)
- use for loop when you need index otherwise foreach
  - or: foreach (string name in names.Select((value, index) => new { value, index }))
- Array.Sort() Array.Reverse() Array.IndexOf()
- multi-dimensional arrays:
  ```cs
  int[,] grid = new int[3, 4]; // 3 rows, 4 columns
  //   GetLength(0) → number of rows
  //   GetLength(1) → number of columns
  ```
- jagged arrays (array of arrays):
  ```cs
  int[][] jagged = new int[3][3]; // 3 rows, 4 columns
  ```

**Switch**

- have to use break/return/throw
- possible to stack case labels
- switch expressions (shorthand):
  ```cs
  return mark switch
  {
      >= 70 => "A",
      >= 60 or <second_check> => "B",
      >= 50 and <second_check> => "C",
      _ => "F",
  };
  ```

**List**

- .Count

**OOP**

- struct vs class:
  - struct creates deep copy when assigned or passed to function (fix by returning new copy in function)
  - class is single shared instance (default to classes)
- protected: not accessible outside (object) the class/inherited classes
- always try to keep logic to private variables and keep public variables small, and only expose what you need (so we don't have to change every implementation when refactoring public variable)
- abstract class: cannot create object of class
- virtual: declare that class can be overridden by children
- use structs for small data, e.g. coordinates, money, date ranges
  - use classes when data gets more complex than that
- polymorphism: overriding parent methods
- abstract class: cannot create object of class, must be inherited (incomplete implementation)
- interface: describing functionality, e.g. ISearchable, IEnumerable

**Enums**

- increasing int under the hood
- ToString() returns name not int

**Exceptions**

- try/catch
- use specific built-in csharp error (Exception for all, avoid)

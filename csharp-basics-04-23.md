# C# Basics - Day 1

[GitHub Repo](https://github.com/boolean-uk/csharp-4day-course)

## how to create a fresh console program in Visual Studio (not file based, but solution based)

1. Open Visual Studio
2. Create a new 'Console App'
3. Alternatively, use `dotnet new console`

**File-based:** Single .cs file, no project file
**Project-based:** Multiple .cs files, with a .csproj file to manage them

```cs
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World");
    }
}
```

`dotnet run hello-world.cs`
`dotnet build hello-world.cs`

## what Program.cs is and why we can avoid a Main function (top level statements)

One file in each project can contain top-level statements instead of a Main function, with the entrypoint being the first line of code. Same as using a Main function, but less boilerplate. Program.cs is the default entrypoint file. (can only have one entrypoint)

## defining variables of different types to store strings, integers, booleans, arrays

[Cheatsheet](https://github.com/milanm/csharp-cheatsheet)

### Comments

```cs
// This is a single-line comment

/* This is a
   multi-line comment */
```

### Strings

```cs
string greeting = "Hello";
string name = "World";
string message = greeting + " " + name; // "Hello World"
string interpolated = $"{greeting} {name}!"; // "Hello World!"

// Common string methods
bool contains = text.Contains("World"); // true
string upper = text.ToUpper(); // "HELLO, WORLD!"
string lower = text.ToLower(); // "hello, world!"
string replaced = text.Replace("Hello", "Hi"); // "Hi, World!"
string trimmed = "  text  ".Trim(); // "text"
string[] split = text.Split(','); // ["Hello", " World!"]
int length = text.Length; // 13
```

### Basic types

```cs
// Integers
byte byteValue = 255;                // 8-bit unsigned integer (0 to 255)
sbyte sbyteValue = -128;             // 8-bit signed integer (-128 to 127)
short shortValue = -32768;           // 16-bit signed integer (-32,768 to 32,767)
ushort ushortValue = 65535;          // 16-bit unsigned integer (0 to 65,535)
int intValue = -2147483648;          // 32-bit signed integer (-2,147,483,648 to 2,147,483,647)
uint uintValue = 4294967295;         // 32-bit unsigned integer (0 to 4,294,967,295)
long longValue = -9223372036854775808; // 64-bit signed integer (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807)
ulong ulongValue = 18446744073709551615; // 64-bit unsigned integer (0 to 18,446,744,073,709,551,615)

// Floats
float floatValue = 3.14f;            // 32-bit floating-point (7 significant digits precision)
double doubleValue = 3.14159265359;  // 64-bit floating-point (15-16 significant digits precision)
decimal decimalValue = 3.14159265359m; // 128-bit high-precision decimal (28-29 significant digits)

// Booleans
bool isTrue = true;
bool isFalse = false;

// Chars
char letter = 'A';
char unicodeChar = '\u0041'; // Unicode character for 'A'
char escapeChar = '\n'; // Newline

// DateTime and TimeSpan
DateTime now = DateTime.Now;
DateTime utcNow = DateTime.UtcNow;
DateOnly today = DateOnly.FromDateTime(DateTime.Today); // Date without time (C# 10+)
TimeOnly noon = new TimeOnly(12, 0, 0);                 // Time without date (C# 10+)
DateTime specific = new DateTime(2023, 1, 1);
TimeSpan oneHour = TimeSpan.FromHours(1);
TimeSpan duration = TimeSpan.FromMinutes(90);

// Nullable types (can be null)
int? nullableInt = null;
bool? nullableBool = null;

// Vars (inferred)
var inferredInt = 42;                // Compiler infers int
var inferredString = "Hello";        // Compiler infers string
var inferredList = new List<int>();  // Compiler infers List<int>

// Compile-time constants (must be primitive types or string)
const double Pi = 3.14159;
const string AppName = "MyApp";

// Runtime constants
readonly DateTime StartTime = DateTime.Now;
public static readonly HttpClient SharedClient = new HttpClient(); // initialized only once at runtime
```

### Arrays (fixed size)

```cs
int[] numbers = new int[5];                      // Array of 5 integers with default values (0)
int[] initialized = new int[] { 1, 2, 3, 4, 5 }; // Initialized array
int[] shorthand = { 1, 2, 3, 4, 5 };             // Shorthand initialization

// Accessing elements
int firstNumber = numbers[0];                     // First element
numbers[0] = 10;                                  // Assign value to first element

// Array properties and methods
int length = numbers.Length;                      // Number of elements
Array.Sort(numbers);                              // Sort array in-place
Array.Reverse(numbers);                           // Reverse array in-place
int index = Array.IndexOf(names, "Bob");          // Find index of element
bool exists = Array.Exists(numbers, n => n > 10); // Check if condition exists
```

### Lists (dynamic size)

```cs
using System.Collections.Generic;

List<string> names = new List<string>();          // Empty list
List<int> numbers = new List<int> { 1, 2, 3 };    // Initialized list

// Add elements
names.Add("Alice");                               // Add single element
names.AddRange(new[] { "Bob", "Charlie" });       // Add multiple elements

// Access elements
string first = names[0];                          // Access by index
names[0] = "Alicia";                              // Modify by index

// Remove elements
names.Remove("Bob");                              // Remove specific element
names.RemoveAt(0);                                // Remove element at index
names.RemoveAll(x => x.StartsWith("C"));          // Remove all that match condition
names.Clear();                                    // Remove all elements

// Search and query
bool contains = numbers.Contains(2);              // Check if contains value
int index = numbers.IndexOf(3);                   // Find index of element
List<int> filtered = numbers.FindAll(n => n > 1); // Find all matching elements
int found = numbers.Find(n => n > 2);             // Find first matching element

// Other operations
int count = numbers.Count;                       // Number of elements
numbers.Sort();                                  // Sort list in-place
numbers.Reverse();                               // Reverse list in-place
numbers.ForEach(n => Console.WriteLine(n));      // Perform action on each element
```

### Dictionary (key-value pairs, similar to Python dict or JavaScript object)

```cs
using System.Collections.Generic;

Dictionary<string, int> ages = new Dictionary<string, int>();
Dictionary<string, string> capitals = new Dictionary<string, string>
{
    { "USA", "Washington D.C." },
    { "UK", "London" },
    ["France"] = "Paris"              // Alternative initialization syntax
};

// Add entries
ages.Add("Alice", 30);
ages["Bob"] = 25;                     // Add or update using indexer

// Access values
int aliceAge = ages["Alice"];         // Access by key (throws if not found)
bool success = ages.TryGetValue("Charlie", out int charlieAge); // Safe access

// Check existence
bool containsKey = ages.ContainsKey("Alice");
bool containsValue = ages.ContainsValue(25);

// Remove entries
bool removed = ages.Remove("Bob");

// Iterate through dictionary
foreach (KeyValuePair<string, int> pair in ages)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

// Or using deconstruction (C# 7.0+)
foreach (var (name, age) in ages)
{
    Console.WriteLine($"{name}: {age}");
}
```

## Methods and Functions

- must live inside Classes - this is a core difference with Python, Javascript where you can have functions defined freely
- it is possible to define functions inside other functions (local functions)

```cs
// Instance method
public int Add(int a, int b)
{
    return a + b;
}

// Static method
public static double CalculateArea(double radius)
{
    return Math.PI * radius * radius;
}

// Void method (no return value)
public void PrintMessage(string message)
{
    Console.WriteLine(message);
}

// Optional parameters
public void Greet(string name, string greeting = "Hello")
{
    Console.WriteLine($"{greeting}, {name}!");
}

// Named arguments
Greet(greeting: "Hi", name: "Alice");
```

## Classes and Objects

```cs
// Basic class definition
public class Person
{
    // Fields
    private string name;
    private int age;

    // Properties
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Auto-implemented property
    public int Age { get; set; }

    // Read-only property
    public bool IsAdult => Age >= 18;

    // Constructors
    public Person()
    {
        // Default constructor
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Methods
    public void Introduce()
    {
        Console.WriteLine($"Hello, my name is {Name} and I'm {Age} years old.");
    }

    public string GetDescription() => $"{Name}, {Age} years old";

    // Static members
    public static int MinimumAge { get; } = 0;

    public static bool IsValidAge(int age)
    {
        return age >= MinimumAge;
    }
}

// Usage
Person person = new Person();
person.Name = "Alice";
person.Age = 30;
person.Introduce();

Person bob = new Person("Bob", 25);
string description = bob.GetDescription();
bool isAdult = bob.IsAdult;

bool isValid = Person.IsValidAge(20);
```

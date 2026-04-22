# Day 1

## Goals by end of morning day 1

1. .NET and Visual Studio Community editions installed
2. Read through the basics C# guides on Microsoft's documentation site
3. Forked the practice repository and submitted the exercise solutions

## 1. Install Visual Studio & ASP.NET (~1hr)

1. Download and install [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community/)
2. You can follow the [official installation guide](https://learn.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=visualstudio#step-4---choose-workloads) making sure to select "ASP.NET and web development" and the `.NET desktop development` workloads. Make sure to also go to `Individual Components` -> `.NET 10 Runtime` (see screenshot)
3. Verify installation by launching Visual Studio on your Windows machine


![.NET 10 Runtime](screenshots/net_10_runtime.png)

## 2. Language tour (reading ~25 min)

Please *read fully* the following guides, ~25 min

- [ ] [C# Overview](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/overview) -> this explains file based or project based apps (we will use project based)
- [ ] [Program structure](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/) -> we will use project based apps with a `Program.cs` that uses **top level statements** instead of a `Main` method
- [ ] [Namespaces and using directives](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/namespaces)
- [ ] [Top level statements detail](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements)
- [ ] [Type system overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/) -> C# is a typed language, so we need to learn about types and how to use them
- [ ] [Built-in types](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/built-in-types) -> continuation from overview
- [ ] [Generics (lists)](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics)
- [ ] [Classes](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes)
- [ ] [OOP](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/)
- [ ] [OOP - objects](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/objects)
- [ ] [Exceptions and errors overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/)

Additional useful reading:
- [ ] [Differences for python developers](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tips-for-python-developers) -> important bits are: syntax, use of tokens and `;` to separate code blocks (similar to JS), generics and nullable types
- [ ] [Differences for javascript developers](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tips-for-javascript-developers) -> syntax, async/await (will be useful towards end of our lessons)

You should be familiar, by the end of the reading, with the following key concepts:

- how to create a fresh console program in Visual Studio (not file based, but solution based)
- what Program.cs is and why we can avoid a Main function (top level statements)
- defining variables of different types to store strings, integers, booleans, arrays
- syntax for defining functions (must live inside Classes - this is a core difference with Python, Javascript where you can have functions defined freely)
- creating an instance of a class (object) => see fundamentals exercises below
- Arrays in C# are fixed size and need to be resized, which is why we use List<T> generic type which is a convenient wrapper on top of arrays
- defining generics variables (ie. List) which allow us to use dynamic lists

Please refer to the links above as official documentation throughout the 4 days of the course.

## 3. Fundamentals practice exercises

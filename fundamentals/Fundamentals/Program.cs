using Fundamentals.Exercises;

Console.WriteLine("=== NUMBERS — examples ===\n");

Console.WriteLine("-- Lesson A: integers --");
Console.WriteLine($"Double(21) = {Numbers.Double(21)}");

Console.WriteLine("\n-- Lesson B: variables --");
Console.WriteLine($"CountUpToFive() = {Numbers.CountUpToFive()}");

Console.WriteLine("\n-- Lesson C: doubles --");
Console.WriteLine($"CircleArea(2.0) = {Numbers.CircleArea(2.0)}");

Console.WriteLine("\n-- Lesson D: integer division truncates --");
Console.WriteLine($"DivideIntegers(7, 2) = {Numbers.DivideIntegers(7, 2)}   ← fractional part lost!");

Console.WriteLine("\n-- Lesson E: casting --");
Console.WriteLine($"IntToDouble(5)         = {Numbers.IntToDouble(5)}");
Console.WriteLine($"DoubleToInt(3.9)       = {Numbers.DoubleToInt(3.9)}   ← truncated, not rounded");
Console.WriteLine($"DivideWithCasting(7,2) = {Numbers.DivideWithCasting(7, 2)}  ← fixed!");

Console.WriteLine("\n=== NUMBERS — your exercises ===");
Console.WriteLine("Implement each method in Exercises/Numbers.cs, then uncomment below.\n");

// Console.WriteLine($"Add(3, 4)                 = {Numbers.Add(3, 4)}");
// Console.WriteLine($"RectangleArea(3, 4)       = {Numbers.RectangleArea(3, 4)}");
// Console.WriteLine($"CelsiusToFahrenheit(100)  = {Numbers.CelsiusToFahrenheit(100)}");

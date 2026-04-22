using System.Globalization;
using Lessons = Fundamentals.Lessons;
using Exercises = Fundamentals.Exercises;

// Pin culture so format specifiers like :C render £ rather than the host's local currency.
CultureInfo.CurrentCulture = new CultureInfo("en-GB");

Console.WriteLine("=== NUMBERS — examples ===\n");

Console.WriteLine("-- Lesson A: integers --");
Console.WriteLine($"Double(21) = {Lessons.Numbers.Double(21)}");

Console.WriteLine("\n-- Lesson B: variables --");
Console.WriteLine($"CountUpToFive() = {Lessons.Numbers.CountUpToFive()}");

Console.WriteLine("\n-- Lesson C: doubles --");
Console.WriteLine($"CircleArea(2.0) = {Lessons.Numbers.CircleArea(2.0)}");

Console.WriteLine("\n-- Lesson D: integer division truncates --");
Console.WriteLine($"DivideIntegers(7, 2) = {Lessons.Numbers.DivideIntegers(7, 2)}   ← fractional part lost!");

Console.WriteLine("\n-- Lesson E: casting --");
Console.WriteLine($"IntToDouble(5)         = {Lessons.Numbers.IntToDouble(5)}");
Console.WriteLine($"DoubleToInt(3.9)       = {Lessons.Numbers.DoubleToInt(3.9)}   ← truncated, not rounded");
Console.WriteLine($"DivideWithCasting(7,2) = {Lessons.Numbers.DivideWithCasting(7, 2)}  ← fixed!");

Console.WriteLine("\n=== NUMBERS — your exercises ===");
Console.WriteLine("Implement each method in Exercises/Numbers.cs, then uncomment below.\n");

// Console.WriteLine($"Add(3, 4)                 = {Exercises.Numbers.Add(3, 4)}");
// Console.WriteLine($"RectangleArea(3, 4)       = {Exercises.Numbers.RectangleArea(3, 4)}");
// Console.WriteLine($"CelsiusToFahrenheit(100)  = {Exercises.Numbers.CelsiusToFahrenheit(100)}");


Console.WriteLine("\n\n=== STRINGS — examples ===\n");

Console.WriteLine("-- Lesson A: char vs string --");
Console.WriteLine($"FirstLetter(\"hello\")           = '{Lessons.Strings.FirstLetter("hello")}'   ← a char, not a string");
Console.WriteLine($"AddCharsAsNumbers('a', 'b')    = {Lessons.Strings.AddCharsAsNumbers('a', 'b')}   ← char + char is int arithmetic!");
Console.WriteLine($"AddStringsAsText(\"a\", \"b\")     = \"{Lessons.Strings.AddStringsAsText("a", "b")}\"  ← string + string concatenates");

Console.WriteLine("\n-- Lesson B: three quote styles --");
Console.WriteLine($"RegularLiteral()  →");
Console.WriteLine(Lessons.Strings.RegularLiteral());
Console.WriteLine($"\nWindowsPath_RegularString()  = {Lessons.Strings.WindowsPath_RegularString()}   ← each \\ had to be written as \\\\");
Console.WriteLine($"WindowsPath_VerbatimString() = {Lessons.Strings.WindowsPath_VerbatimString()}   ← @\"...\" — backslashes are literal");
Console.WriteLine($"VerbatimWithQuotes()         = {Lessons.Strings.VerbatimWithQuotes()}   ← inside @\"...\" use \"\" to get a literal quote");
Console.WriteLine($"\nRawLiteral() →");
Console.WriteLine(Lessons.Strings.RawLiteral());
Console.WriteLine($"\nInterpolated(\"Dogezen\", 42) → {Lessons.Strings.Interpolated("Dogezen", 42)}");

Console.WriteLine("\n-- Lesson C: immutability --");
Console.WriteLine($"ImmutabilityGotcha(\"hello\")  = \"{Lessons.Strings.ImmutabilityGotcha("hello")}\"  ← ToUpper() result was discarded");
Console.WriteLine($"ImmutabilityFixed(\"hello\")   = \"{Lessons.Strings.ImmutabilityFixed("hello")}\"  ← result captured with =");

Console.WriteLine("\n-- Lesson D: comparison --");
Console.WriteLine($"SameExact(\"Hello\", \"hello\")           = {Lessons.Strings.SameExact("Hello", "hello")}   ← case-sensitive");
Console.WriteLine($"SameIgnoringCase(\"Hello\", \"hello\")    = {Lessons.Strings.SameIgnoringCase("Hello", "hello")}    ← case-insensitive");
Console.WriteLine($"HasContent(\"   \")                     = {Lessons.Strings.HasContent("   ")}   ← whitespace doesn't count");
Console.WriteLine($"HasContent(\"hi\")                      = {Lessons.Strings.HasContent("hi")}");

Console.WriteLine("\n-- Lesson E: parsing --");
Console.WriteLine($"ParseOrCrash(\"42\")       = {Lessons.Strings.ParseOrCrash("42")}");
try { Lessons.Strings.ParseOrCrash("abc"); }
catch (FormatException) { Console.WriteLine($"ParseOrCrash(\"abc\")     threw FormatException  ← bad input crashes"); }
Console.WriteLine($"TryParseSafely(\"42\")    = {(Lessons.Strings.TryParseSafely("42", out int good) ? $"true, value = {good}" : "false")}");
Console.WriteLine($"TryParseSafely(\"abc\")   = {(Lessons.Strings.TryParseSafely("abc", out int _) ? "true" : "false")}  ← no exception");

Console.WriteLine("\n-- Lesson F: format specifiers --");
Console.WriteLine($"PriceAsCurrency(19.99m)  = {Lessons.Strings.PriceAsCurrency(19.99m)}");
Console.WriteLine($"RoundedToTwo(3.14159)    = {Lessons.Strings.RoundedToTwo(3.14159)}");
Console.WriteLine($"PaddedInteger(42)        = {Lessons.Strings.PaddedInteger(42)}");

Console.WriteLine("\n=== STRINGS — your exercises ===");
Console.WriteLine("Implement each method in Exercises/Strings.cs, then uncomment below.\n");

// Console.WriteLine($"Shout(\"hello\")              = \"{Exercises.Strings.Shout("hello")}\"");
// Console.WriteLine($"CountVowels(\"Hello World\")  = {Exercises.Strings.CountVowels("Hello World")}");
// Console.WriteLine($"IsPalindrome(\"Racecar\")     = {Exercises.Strings.IsPalindrome("Racecar")}");
// Console.WriteLine($"FormatPrice(19.99m)         = \"{Exercises.Strings.FormatPrice(19.99m)}\"");
// Console.WriteLine($"SafeParseInt(\"abc\")         = {Exercises.Strings.SafeParseInt("abc")}");


Console.WriteLine("\n\n=== STRINGS (advanced — tackle these last) ===\n");

Console.WriteLine("-- Lesson G: StringBuilder --");
string[] parts = ["one", "two", "three", "four"];
Console.WriteLine($"JoinWithConcatenation(parts)  = \"{Lessons.StringsAdvanced.JoinWithConcatenation(parts)}\"   ← allocates each iter");
Console.WriteLine($"JoinWithStringBuilder(parts)  = \"{Lessons.StringsAdvanced.JoinWithStringBuilder(parts)}\"   ← one buffer, one final string");
Console.WriteLine($"JoinWithStringJoin(parts)     = \"{Lessons.StringsAdvanced.JoinWithStringJoin(parts)}\"   ← simplest when you just need to join");

Console.WriteLine("\n=== STRINGS (advanced) — your exercise ===");
Console.WriteLine("Implement ParseCsvRow in Exercises/StringsAdvanced.cs, then uncomment below.\n");

// Console.WriteLine($"ParseCsvRow(\"one,two,three\")              = [{string.Join(" | ", Exercises.StringsAdvanced.ParseCsvRow("one,two,three"))}]");
// Console.WriteLine($"ParseCsvRow(\"\\\"Hello, world\\\",foo,bar\")  = [{string.Join(" | ", Exercises.StringsAdvanced.ParseCsvRow("\"Hello, world\",foo,bar"))}]");
// Console.WriteLine($"ParseCsvRow(\"a,,b\")                       = [{string.Join(" | ", Exercises.StringsAdvanced.ParseCsvRow("a,,b"))}]");


Console.WriteLine("\n\n=== ARRAYS — examples ===\n");

Console.WriteLine("-- Lesson A: declaring arrays --");
Console.WriteLine($"EmptyWithSize().Length          = {Lessons.Arrays.EmptyWithSize().Length}   ← size-only, contents zero-filled");
Console.WriteLine($"LonghandInitialiser()           = [{string.Join(", ", Lessons.Arrays.LonghandInitialiser())}]");
Console.WriteLine($"ShorthandInitialiser()          = [{string.Join(", ", Lessons.Arrays.ShorthandInitialiser())}]");
Console.WriteLine($"CollectionExpression()          = [{string.Join(", ", Lessons.Arrays.CollectionExpression())}]   ← C# 12+ form");

Console.WriteLine("\n-- Lesson B: strong typing --");
Console.WriteLine($"IntsOnly()                      = [{string.Join(", ", Lessons.Arrays.IntsOnly())}]");
Console.WriteLine($"StringsOnly()                   = [{string.Join(", ", Lessons.Arrays.StringsOnly())}]   ← can't mix int + string in one array");

Console.WriteLine("\n-- Lesson C: default values --");
Console.WriteLine($"DefaultInts()                   = [{string.Join(", ", Lessons.Arrays.DefaultInts())}]");
Console.WriteLine($"DefaultDoubles()                = [{string.Join(", ", Lessons.Arrays.DefaultDoubles())}]");
Console.WriteLine($"DefaultBools()                  = [{string.Join(", ", Lessons.Arrays.DefaultBools())}]");
Console.WriteLine($"DefaultStrings()                = [{string.Join(", ", Lessons.Arrays.DefaultStrings().Select(s => s ?? "null"))}]   ← reference types default to null");

Console.WriteLine("\n-- Lesson D: indexing, Length, mutation --");
int[] demo = { 10, 20, 30 };
Console.WriteLine($"FirstElement([10,20,30])        = {Lessons.Arrays.FirstElement(demo)}");
Console.WriteLine($"LastElement([10,20,30])         = {Lessons.Arrays.LastElement(demo)}");
Console.WriteLine($"HowMany([10,20,30])             = {Lessons.Arrays.HowMany(demo)}");
Lessons.Arrays.SetFirst(demo, 999);
Console.WriteLine($"after SetFirst(demo, 999)        demo is now [{string.Join(", ", demo)}]   ← caller's array was mutated");

Console.WriteLine("\n-- Lesson E: out of bounds --");
try { Lessons.Arrays.AccessOutOfBounds(new[] { 1, 2, 3 }); }
catch (IndexOutOfRangeException) { Console.WriteLine("AccessOutOfBounds([1,2,3])       threw IndexOutOfRangeException   ← no silent undefined"); }
Console.WriteLine($"SafeGet([10,20,30], 10, -1)     = {Lessons.Arrays.SafeGet(new[] { 10, 20, 30 }, 10, -1)}   ← range-checked, returns fallback");

Console.WriteLine("\n-- Lesson F: iteration --");
int[] numbers = { 1, 2, 3, 4, 5 };
Console.WriteLine($"SumWithForeach([1..5])          = {Lessons.Arrays.SumWithForeach(numbers)}");
Console.WriteLine($"SumWithFor([1..5])              = {Lessons.Arrays.SumWithFor(numbers)}   ← same result, different loop style");
Console.WriteLine($"FillWithIndexSquares(5)         = [{string.Join(", ", Lessons.Arrays.FillWithIndexSquares(5))}]");

Console.WriteLine("\n-- Lesson G: utilities (in-place!) --");
int[] toSort = { 3, 1, 2 };
Lessons.Arrays.SortInPlace(toSort);
Console.WriteLine($"after SortInPlace([3,1,2])       the input is now [{string.Join(", ", toSort)}]");
int[] toReverse = { 1, 2, 3 };
Lessons.Arrays.ReverseInPlace(toReverse);
Console.WriteLine($"after ReverseInPlace([1,2,3])    the input is now [{string.Join(", ", toReverse)}]");
Console.WriteLine($"FindIndexOf([10,20,30], 20)     = {Lessons.Arrays.FindIndexOf(new[] { 10, 20, 30 }, 20)}");
Console.WriteLine($"FindIndexOf([10,20,30], 99)     = {Lessons.Arrays.FindIndexOf(new[] { 10, 20, 30 }, 99)}   ← -1 when not found");

Console.WriteLine("\n=== ARRAYS — your exercises ===");
Console.WriteLine("Implement each method in Exercises/Arrays.cs, then uncomment below.\n");

// Console.WriteLine($"SumOfArray([1,2,3,4])       = {Exercises.Arrays.SumOfArray(new[] { 1, 2, 3, 4 })}");
// Console.WriteLine($"FindLargest([3,1,4,1,5,9])  = {Exercises.Arrays.FindLargest(new[] { 3, 1, 4, 1, 5, 9 })}");
// Console.WriteLine($"CountEvens([1,2,3,4,5,6])   = {Exercises.Arrays.CountEvens(new[] { 1, 2, 3, 4, 5, 6 })}");
// Console.WriteLine($"ReverseArray([1,2,3])       = [{string.Join(", ", Exercises.Arrays.ReverseArray(new[] { 1, 2, 3 }))}]");


Console.WriteLine("\n\n=== ARRAYS (advanced — tackle these last) ===\n");

Console.WriteLine("-- Lesson H: multi-dimensional (int[,]) --");
int[,] grid = Lessons.ArraysAdvanced.GridWithInitialiser();
Console.WriteLine($"GridWithInitialiser rows × cols = {Lessons.ArraysAdvanced.RowCount(grid)} × {Lessons.ArraysAdvanced.ColCount(grid)}");
Console.WriteLine($"GetCell(grid, 1, 2)             = {Lessons.ArraysAdvanced.GetCell(grid, 1, 2)}   ← bracket with comma: grid[row, col]");
Console.WriteLine($"SumAllCells(grid)               = {Lessons.ArraysAdvanced.SumAllCells(grid)}");

Console.WriteLine("\n-- Lesson H: jagged (int[][]) --");
int[][] triangle = Lessons.ArraysAdvanced.MakeTriangle();
Console.WriteLine($"MakeTriangle() rows             = {triangle.Length}   ← outer array length");
Console.WriteLine($"RowLength(triangle, 2)          = {Lessons.ArraysAdvanced.RowLength(triangle, 2)}   ← rows can have different sizes");
Console.WriteLine($"JaggedGet(triangle, 2, 1)       = {Lessons.ArraysAdvanced.JaggedGet(triangle, 2, 1)}   ← two brackets: jagged[row][col]");
Console.WriteLine($"SumAllJagged(triangle)          = {Lessons.ArraysAdvanced.SumAllJagged(triangle)}");

Console.WriteLine("\n=== ARRAYS (advanced) — your exercises ===");
Console.WriteLine("Implement Transpose and HasDuplicates in Exercises/ArraysAdvanced.cs, then uncomment below.\n");

// int[,] m = { { 1, 2, 3 }, { 4, 5, 6 } };
// int[,] t = Exercises.ArraysAdvanced.Transpose(m);
// Console.WriteLine($"Transpose(2×3) → {t.GetLength(0)}×{t.GetLength(1)}");
// Console.WriteLine($"HasDuplicates([1,2,3])     = {Exercises.ArraysAdvanced.HasDuplicates(new[] { 1, 2, 3 })}");
// Console.WriteLine($"HasDuplicates([1,2,1])     = {Exercises.ArraysAdvanced.HasDuplicates(new[] { 1, 2, 1 })}");


Console.WriteLine("\n\n=== CONTROL FLOW — examples ===\n");

Console.WriteLine("-- Lesson A: if / else if / else --");
Console.WriteLine($"DescribeSign(-3) = \"{Lessons.ControlFlow.DescribeSign(-3)}\"");
Console.WriteLine($"DescribeSign(0)  = \"{Lessons.ControlFlow.DescribeSign(0)}\"");
Console.WriteLine($"DescribeSign(7)  = \"{Lessons.ControlFlow.DescribeSign(7)}\"");

Console.WriteLine("\n-- Lesson B: the \"no braces\" gotcha --");
Console.WriteLine($"NoBracesGotcha(0, 0) = {Lessons.ControlFlow.NoBracesGotcha(0, 0)}   ← expected 0, got 10 — second line ran anyway");
Console.WriteLine($"NoBracesGotcha(1, 0) = {Lessons.ControlFlow.NoBracesGotcha(1, 0)}   ← both lines ran");
Console.WriteLine($"NoBracesFixed(0, 0)  = {Lessons.ControlFlow.NoBracesFixed(0, 0)}    ← fixed — both lines skipped");
Console.WriteLine($"NoBracesFixed(1, 0)  = {Lessons.ControlFlow.NoBracesFixed(1, 0)}   ← both lines ran");

Console.WriteLine("\n-- Lesson C: else if chain --");
Console.WriteLine($"Grade(85) = \"{Lessons.ControlFlow.Grade(85)}\"");
Console.WriteLine($"Grade(65) = \"{Lessons.ControlFlow.Grade(65)}\"");
Console.WriteLine($"Grade(55) = \"{Lessons.ControlFlow.Grade(55)}\"");
Console.WriteLine($"Grade(40) = \"{Lessons.ControlFlow.Grade(40)}\"");

Console.WriteLine("\n-- Lesson D: switch statement (break flip) --");
Console.WriteLine($"DayKind(1)  = \"{Lessons.ControlFlow.DayKind(1)}\"");
Console.WriteLine($"DayKind(6)  = \"{Lessons.ControlFlow.DayKind(6)}\"");
Console.WriteLine($"DayKind(99) = \"{Lessons.ControlFlow.DayKind(99)}\"");

Console.WriteLine("\n-- Lesson E: ternary --");
Console.WriteLine($"Parity(4) = \"{Lessons.ControlFlow.Parity(4)}\"");
Console.WriteLine($"Parity(7) = \"{Lessons.ControlFlow.Parity(7)}\"");

Console.WriteLine("\n-- Lesson F: while + break --");
Console.WriteLine($"CountHalvings(8)         = {Lessons.ControlFlow.CountHalvings(8)}");
Console.WriteLine($"FirstMultipleOfThree(10) = {Lessons.ControlFlow.FirstMultipleOfThree(10)}   ← while(true) + break");

Console.WriteLine("\n-- Lesson G: for loop --");
Console.WriteLine($"SumArray([1, 2, 3, 4]) = {Lessons.ControlFlow.SumArray(new[] { 1, 2, 3, 4 })}");

Console.WriteLine("\n-- Lesson H: foreach --");
Console.WriteLine($"MaxValue([3, 7, 1, 9, 4]) = {Lessons.ControlFlow.MaxValue(new[] { 3, 7, 1, 9, 4 })}");

Console.WriteLine("\n=== CONTROL FLOW — your exercises ===");
Console.WriteLine("Implement each method in Exercises/ControlFlow.cs, then uncomment below.\n");

// Console.WriteLine($"Sign(-3)                          = \"{Exercises.ControlFlow.Sign(-3)}\"");
// Console.WriteLine($"IsLeapYear(2024)                  = {Exercises.ControlFlow.IsLeapYear(2024)}");
// Console.WriteLine($"IsLeapYear(1900)                  = {Exercises.ControlFlow.IsLeapYear(1900)}");
// Console.WriteLine($"DayName(3)                        = \"{Exercises.ControlFlow.DayName(3)}\"");
// Console.WriteLine($"IsVowel('E')                      = {Exercises.ControlFlow.IsVowel('E')}");
// Console.WriteLine($"CountPositives([3,-1,0,7,-5])     = {Exercises.ControlFlow.CountPositives(new[] { 3, -1, 0, 7, -5 })}");
// Console.WriteLine($"FirstIndexOf([10,20,30,20], 20)   = {Exercises.ControlFlow.FirstIndexOf(new[] { 10, 20, 30, 20 }, 20)}");
// Console.WriteLine($"SumUntilNegative([1,2,3,-1,10])   = {Exercises.ControlFlow.SumUntilNegative(new[] { 1, 2, 3, -1, 10 })}");
// Console.WriteLine($"ReverseArray([1,2,3,4])           = [{string.Join(", ", Exercises.ControlFlow.ReverseArray(new[] { 1, 2, 3, 4 }))}]");


Console.WriteLine("\n\n=== CONTROL FLOW (advanced — tackle these last) ===\n");

Console.WriteLine("-- Lesson I: switch expression --");
Console.WriteLine($"DayNameShort(1)  = \"{Lessons.ControlFlowAdvanced.DayNameShort(1)}\"");
Console.WriteLine($"DayNameShort(7)  = \"{Lessons.ControlFlowAdvanced.DayNameShort(7)}\"");
Console.WriteLine($"DayNameShort(99) = \"{Lessons.ControlFlowAdvanced.DayNameShort(99)}\"");

Console.WriteLine("\n-- Lesson J: relational patterns --");
Console.WriteLine($"GradeFromMark(85)   = \"{Lessons.ControlFlowAdvanced.GradeFromMark(85)}\"");
Console.WriteLine($"GradeFromMark(63)   = \"{Lessons.ControlFlowAdvanced.GradeFromMark(63)}\"");
Console.WriteLine($"GradeFromMark(40)   = \"{Lessons.ControlFlowAdvanced.GradeFromMark(40)}\"");
Console.WriteLine($"TemperatureBand(-5) = \"{Lessons.ControlFlowAdvanced.TemperatureBand(-5)}\"");
Console.WriteLine($"TemperatureBand(20) = \"{Lessons.ControlFlowAdvanced.TemperatureBand(20)}\"");
Console.WriteLine($"TemperatureBand(30) = \"{Lessons.ControlFlowAdvanced.TemperatureBand(30)}\"");

Console.WriteLine("\n=== CONTROL FLOW (advanced) — your exercises ===");
Console.WriteLine("Implement each method in Exercises/ControlFlowAdvanced.cs, then uncomment below.\n");

// Console.WriteLine($"GradeFromMark(85)           = \"{Exercises.ControlFlowAdvanced.GradeFromMark(85)}\"");
// Console.WriteLine($"TrafficLightAction(\"red\")    = \"{Exercises.ControlFlowAdvanced.TrafficLightAction("red")}\"");
// Console.WriteLine($"TrafficLightAction(\"amber\")  = \"{Exercises.ControlFlowAdvanced.TrafficLightAction("amber")}\"");

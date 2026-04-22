namespace Fundamentals.Lessons;

// Theme: Enums — a named, type-safe set of constants.
//
// Closest analogues from your background:
//   Python:     class Direction(Enum): NORTH = 0; EAST = 1; ...
//   JavaScript: no real equivalent — people fake it with Object.freeze({ ... })
//               or, in TypeScript, a union of string literals.
//
// In C# an enum is first-class: it's its own TYPE, with its own name, and the
// compiler checks that any value passed is one of the declared members.
// Compare with passing around magic strings ("red", "amber", "green") where a
// typo sails through compilation and blows up only at runtime.
//
// Enums pair naturally with the `switch` statement you met in the ControlFlow
// lesson — the compiler knows every possible enum value, which makes switches
// exhaustive, readable, and refactor-safe.
//
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/Enums.cs.

// ─────────────────────────────────────────────────────────────
// LESSON A: Declaring an enum
// ─────────────────────────────────────────────────────────────
// Syntax: `enum Name { Member1, Member2, ... }`. Declared at the namespace
// level (outside any class), just like a struct or a class.
//
// Convention: the type name is PascalCase SINGULAR (TrafficLight, not
// TrafficLights), and members are PascalCase too.
public enum TrafficLight
{
    Red,
    Amber,
    Green,
}

public static class Enums
{
    // ─────────────────────────────────────────────────────────────
    // LESSON B: Using an enum value
    // ─────────────────────────────────────────────────────────────
    // You refer to a member as `TypeName.MemberName`. The variable's type is
    // the enum itself — trying to assign `42` or `"Red"` to a TrafficLight
    // variable is a COMPILE ERROR, not a runtime surprise.

    public static TrafficLight StartOfDay()
    {
        // e.g. returns TrafficLight.Red
        return TrafficLight.Red;
    }

    // Enum values compare with `==` just like ints. No .Equals() ceremony needed.
    public static bool MustStop(TrafficLight light)
    {
        // e.g. MustStop(TrafficLight.Red) == true
        //      MustStop(TrafficLight.Green) == false
        return light == TrafficLight.Red;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON C: switch on an enum — the main payoff
    // ─────────────────────────────────────────────────────────────
    // This is where enums earn their keep. Each `case` names a member of the
    // enum, so the reader sees exactly which values are handled — and if
    // someone later adds a new member to the enum, the `default` branch is
    // the safety net that catches it.
    //
    // Remember from the ControlFlow lesson: every case body must exit
    // explicitly (`return`, `break`, `throw`) — no implicit fall-through.
    public static string Action(TrafficLight light)
    {
        // e.g. Action(TrafficLight.Red)   == "stop"
        //      Action(TrafficLight.Amber) == "ready"
        //      Action(TrafficLight.Green) == "go"
        switch (light)
        {
            case TrafficLight.Red:
                return "stop";
            case TrafficLight.Amber:
                return "ready";
            case TrafficLight.Green:
                return "go";
            default:
                // Defensive: runs only if someone adds a new TrafficLight member
                // and forgets to update this switch. Throwing here is the
                // standard idiom — we'd rather crash loudly than return a
                // silent wrong answer.
                throw new ArgumentOutOfRangeException(nameof(light));
        }
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON D: Underlying int values
    // ─────────────────────────────────────────────────────────────
    // Under the hood, every enum member is backed by an integer. By default
    // the first member is 0, the next is 1, and so on. You can cast between
    // the enum and its underlying int explicitly.

    public static int UnderlyingValueOfRed()
    {
        // e.g. returns 0 — Red is the first member, so its value is 0.
        return (int)TrafficLight.Red;
    }

    public static int UnderlyingValueOfGreen()
    {
        // e.g. returns 2 — Green is the third member (0, 1, 2).
        return (int)TrafficLight.Green;
    }

    // You can also pin explicit values. This is useful when the numbers
    // correspond to something external (HTTP status codes, days of the week
    // as defined by a protocol, etc.).
    public enum HttpStatus
    {
        Ok = 200,
        NotFound = 404,
        ServerError = 500,
    }

    public static int NotFoundCode()
    {
        // e.g. returns 404
        return (int)HttpStatus.NotFound;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON E: Converting between strings and enums
    // ─────────────────────────────────────────────────────────────
    // `ToString()` on an enum gives you the MEMBER NAME (not the int) — handy
    // for logging and user-facing output.
    public static string GreenAsString()
    {
        // e.g. returns "Green"
        return TrafficLight.Green.ToString();
    }

    // Going the other way — parsing a string back into an enum — use
    // `Enum.TryParse<T>`. Same pattern as `int.TryParse`: it returns a bool
    // for success and hands the parsed value back via an `out` parameter.
    // The second argument (`ignoreCase: true`) means "Red", "red", "RED" all
    // parse to TrafficLight.Red — usually what you want for user input.
    public static bool TryParseLight(string text, out TrafficLight light)
    {
        // e.g. TryParseLight("Amber", out var l) → true,  l == TrafficLight.Amber
        //      TryParseLight("purple", out var l) → false, l == default (Red)
        return Enum.TryParse<TrafficLight>(text, ignoreCase: true, out light);
    }
}

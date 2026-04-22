using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Enums.cs.
public class EnumsTests
{
    // ── Lesson B: using enum values ─────────────────────────────────

    [Fact]
    public void LessonB_StartOfDayReturnsRed()
        => Assert.Equal(TrafficLight.Red, Enums.StartOfDay());

    [Theory]
    [InlineData(TrafficLight.Red, true)]
    [InlineData(TrafficLight.Amber, false)]
    [InlineData(TrafficLight.Green, false)]
    public void LessonB_MustStopTrueOnlyForRed(TrafficLight light, bool expected)
        => Assert.Equal(expected, Enums.MustStop(light));

    // ── Lesson C: switch over the enum ──────────────────────────────

    [Theory]
    [InlineData(TrafficLight.Red, "stop")]
    [InlineData(TrafficLight.Amber, "ready")]
    [InlineData(TrafficLight.Green, "go")]
    public void LessonC_ActionMapsEachLight(TrafficLight light, string expected)
        => Assert.Equal(expected, Enums.Action(light));

    // ── Lesson D: underlying int values ─────────────────────────────

    [Fact]
    public void LessonD_RedHasValueZero()
        => Assert.Equal(0, Enums.UnderlyingValueOfRed());

    [Fact]
    public void LessonD_GreenHasValueTwo()
        => Assert.Equal(2, Enums.UnderlyingValueOfGreen());

    [Fact]
    public void LessonD_HttpNotFoundExplicitValue()
        => Assert.Equal(404, Enums.NotFoundCode());

    // ── Lesson E: string ↔ enum ─────────────────────────────────────

    [Fact]
    public void LessonE_GreenToStringIsMemberName()
        => Assert.Equal("Green", Enums.GreenAsString());

    [Theory]
    [InlineData("Red", true, TrafficLight.Red)]
    [InlineData("amber", true, TrafficLight.Amber)]    // case-insensitive
    [InlineData("GREEN", true, TrafficLight.Green)]    // case-insensitive
    [InlineData("purple", false, default(TrafficLight))]
    public void LessonE_TryParseLightHandlesGoodAndBadInput(string text, bool expectedOk, TrafficLight expectedLight)
    {
        bool ok = Enums.TryParseLight(text, out TrafficLight light);
        Assert.Equal(expectedOk, ok);
        if (expectedOk)
        {
            Assert.Equal(expectedLight, light);
        }
    }
}

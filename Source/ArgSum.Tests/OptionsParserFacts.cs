using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ArgSum.Tests
{
    public class OptionsParserFacts
    {
        [Fact]
        public void Force_WhenPassingForceArgument_ForceIsTrue()
        {
            var args = new List<string>
            {
                "--force"
            };

            var options = OptionsParser.Parse(args);

            options.Force.Should().BeTrue();
        }

        [Fact]
        public void Force_WhenNotPassingForceArgument_ForceIsFalse()
        {
            var args = new List<string>
            {
                "some-filename.txt"
            };

            var options = OptionsParser.Parse(args);

            options.Force.Should().BeFalse();
        }

        [Fact]
        public void Help_WhenPassingNoArguments_ShouldDisplayHelp()
        {
            var args = new List<string>();

            var options = OptionsParser.Parse(args);

            options.DisplayHelp.Should().BeTrue();
        }

        [Fact]
        public void Help_WhenPassingHelpArgument_ShouldDisplayHelpAndNothingElse()
        {
            var args = new List<string>
            {
                "--help"
            };

            var options = OptionsParser.Parse(args);

            options.DisplayHelp.Should().BeTrue();
        }

        [Fact]
        public void FileName_WhenPassingFileName_Should()
        {
            var args = new List<string>
            {
                "some-filename.txt"
            };

            var options = OptionsParser.Parse(args);

            options.FileName.Should().Be("some-filename.txt");
            options.Force.Should().BeFalse();
            options.DisplayHelp.Should().BeFalse();
        }

        [Fact]
        public void Pause_WhenPassingPauseFlag_ShouldSetFlagToTrue()
        {
            var args = new List<string>
            {
                "some-filename.txt",
                "--pause"
            };

            var options = OptionsParser.Parse(args);

            options.FileName.Should().Be("some-filename.txt");
            options.Pause.Should().BeTrue();
            options.DisplayHelp.Should().BeFalse();
        }

        [Fact]
        public void UnknownStuff_UnrecognizedInput_ShouldBeTrue()
        {
            var args = new List<string>
            {
                "some-filename.txt",
                "other-filename.txt"
            };

            var options = OptionsParser.Parse(args);

            options.UnrecognizedInput.Should().BeTrue();
        }
    }
}

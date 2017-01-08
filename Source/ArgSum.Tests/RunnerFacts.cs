using NSubstitute;
using Xunit;

namespace ArgSum.Tests
{
    public class RunnerFacts
    {
        [Fact]
        public void NoFileEntered_ShowMessage_NothingElse()
        {
            var options = new Options
            {
                FileName = string.Empty
            };
            var runner = TestableRunner.Create();

            runner.Run(options);

            runner.Console.Received().WriteFileArgumentMissingMessage();
            runner.Calculator.DidNotReceive().UpdateChecksum(Arg.Any<string>());
        }

        [Fact]
        public void FileDoesNotExist_ShowMessage_DontUpdateChecksum()
        {
            var options = new Options
            {
                FileName = "anyfile.txt"
            };
            var runner = TestableRunner.Create()
                .WithMissingFile();

            runner.Run(options);

            runner.Console.Received().WriteFileDoesNotExistMessage(Arg.Any<string>());
            runner.Calculator.DidNotReceive().UpdateChecksum(Arg.Any<string>());
        }

        [Fact]
        public void DisplayHelp_ShowHelp_NothingElse()
        {
            var options = new Options
            {
                DisplayHelp = true
            };
            var runner = TestableRunner.Create();

            runner.Run(options);

            runner.Console.Received().WriteHelpMessage();
            runner.Calculator.DidNotReceive().UpdateChecksum(Arg.Any<string>());
        }

        [Fact]
        public void FileIsKnownSize_DisplayMessage_UpdateChecksum()
        {
            var options = new Options
            {
                FileName = "Track.dat"
            };
            var runner = TestableRunner.Create()
                .WithKnownFileSize();

            runner.Run(options);

            runner.Console.Received().WriteIdentifiedFileMessage(Arg.Is<string>(a => a.Contains("Phoenix")));
            runner.Calculator.Received().UpdateChecksum("Track.dat");
        }

        [Fact]
        public void FileIsProbablyTrackFile_DisplayMessage_UpdateChecksum()
        {
            var options = new Options
            {
                FileName = "Track.dat"
            };
            var runner = TestableRunner.Create()
                .WithProbableFileSize();

            runner.Run(options);

            runner.Console.Received().WriteProbableFileMessage(Arg.Is<string>(a => a.Contains("Track File?")));
            runner.Calculator.Received().UpdateChecksum("Track.dat");
        }

        [Fact]
        public void FileIsNotKnown_DisplayMessage_ForceTrue_UpdateChecksum()
        {
            var options = new Options
            {
                FileName = "Somefile.dat",
                Force = true
            };
            var runner = TestableRunner.Create()
                .WithUnknownFileSize();

            runner.Run(options);

            runner.Console.Received().WriteUnknownFileMessage();
            runner.Calculator.Received().UpdateChecksum("Somefile.dat");
        }

        [Fact]
        public void FileIsNotKnown_DisplayMessage_ForceFalse_ConfirmYes_UpdateChecksum()
        {
            var options = new Options
            {
                FileName = "Somefile.dat",
                Force = false
            };
            var runner = TestableRunner.Create()
                .WithUnknownFileSize()
                .WithProceedAnyway();

            runner.Run(options);

            runner.Console.Received().WriteUnknownFileMessage();
            runner.Calculator.Received().UpdateChecksum("Somefile.dat");
        }

        [Fact]
        public void FileIsNotKnown_DisplayMessage_ForceFalse_ConfirmNo_DoNotUpdateChecksum()
        {
            var options = new Options
            {
                FileName = "Somefile.dat",
                Force = false
            };
            var runner = TestableRunner.Create()
                .WithUnknownFileSize()
                .WithDoNotProceed();

            runner.Run(options);

            runner.Console.Received().WriteUnknownFileMessage();
            runner.Calculator.DidNotReceive().UpdateChecksum("Somefile.dat");
        }

        [Fact]
        public void UnrecognizedInput_ShowMessage_DoNothingElse()
        {
            var options = new Options
            {
                UnrecognizedInput = true
            };
            var runner = TestableRunner.Create()
                .WithUnknownFileSize()
                .WithDoNotProceed();

            runner.Run(options);

            runner.Console.Received().WriteUnrecognizedInputMessage();
            runner.Calculator.DidNotReceive().UpdateChecksum("Somefile.dat");
        }

        [Fact]
        public void PauseTrue_WaitForConfirmationWhenFinished()
        {
            var options = new Options
            {
                Pause = true,
                Force = true,
                FileName = "trackfile.dat"
            };
            var runner = TestableRunner.Create()
                .WithKnownFileSize();

            runner.Run(options);

            runner.Calculator.Received().UpdateChecksum("trackfile.dat");
            runner.Console.Received().WritePauseMessage();
        }
    }
}

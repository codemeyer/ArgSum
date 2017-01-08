using NSubstitute;

namespace ArgSum.Tests
{
    public class TestableRunner : Runner
    {
        public ChecksumCalculator Calculator { get; }
        public FileSystem FileSystem { get; }
        public InputOutput Console { get; }

        public static TestableRunner Create()
        {
            var checksumCalculator = Substitute.For<ChecksumCalculator>();
            var fileSystem = Substitute.For<FileSystem>();
            var console = Substitute.For<InputOutput>();

            return new TestableRunner(checksumCalculator, fileSystem, console);
        }

        public TestableRunner(ChecksumCalculator checksumCalculator, FileSystem fileSystem, InputOutput console)
            : base(checksumCalculator, fileSystem, console)
        {
            Calculator = checksumCalculator;
            FileSystem = fileSystem;
            FileSystem.FileExists(Arg.Any<string>()).Returns(true);
            Console = console;
            Console.WriteLine(Arg.Any<string>());
        }

        public TestableRunner WithMissingFile()
        {
            FileSystem.FileExists(Arg.Any<string>()).Returns(false);
            return this;
        }

        public TestableRunner WithKnownFileSize()
        {
            FileSystem.GetFileLength(Arg.Any<string>()).Returns(16924);
            return this;
        }

        public TestableRunner WithUnknownFileSize()
        {
            FileSystem.GetFileLength(Arg.Any<string>()).Returns(123456);
            return this;
        }

        public TestableRunner WithProbableFileSize()
        {
            FileSystem.GetFileLength(Arg.Any<string>()).Returns(18000);
            return this;
        }

        public TestableRunner WithProceedAnyway()
        {
            Console.ReadKey().Returns('y');
            return this;
        }

        public TestableRunner WithDoNotProceed()
        {
            Console.ReadKey().Returns('n');
            return this;
        }
    }
}

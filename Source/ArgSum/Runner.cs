namespace ArgSum
{
    public class Runner
    {
        private readonly ChecksumCalculator _checksumCalculator;
        private readonly FileSystem _fileSystem;
        private readonly InputOutput _console;

        public Runner(ChecksumCalculator checksumCalculator, FileSystem fileSystem, InputOutput console)
        {
            _checksumCalculator = checksumCalculator;
            _fileSystem = fileSystem;
            _console = console;
        }

        public void Run(Options options)
        {
            if (options.UnrecognizedInput)
            {
                _console.WriteUnrecognizedInputMessage();
                return;
            }

            if (options.DisplayHelp)
            {
                _console.WriteHelpMessage();
                return;
            }

            if (string.IsNullOrWhiteSpace(options.FileName))
            {
                _console.WriteFileArgumentMissingMessage();
                return;
            }

            if (!_fileSystem.FileExists(options.FileName))
            {
                _console.WriteFileDoesNotExistMessage(options.FileName);
                return;
            }

            var fileLength = _fileSystem.GetFileLength(options.FileName);
            var file = KnownFiles.IsKnownFile(fileLength);

            if (file.IsKnown == KnownState.Exact)
            {
                _console.WriteIdentifiedFileMessage(file.Description);
            }
            else if (file.IsKnown == KnownState.Probable)
            {
                _console.WriteProbableFileMessage(file.Description);
            }
            else
            {
                _console.WriteUnknownFileMessage();

                if (!ShouldProceed(options.Force))
                {
                    _console.WriteDidNotProceedWithUnknownFileMessage();
                    return;
                }
            }

            _checksumCalculator.UpdateChecksum(options.FileName);

            _console.WriteSuccessMessage();

            if (options.Pause)
            {
                _console.WritePauseMessage();
            }
        }

        private bool ShouldProceed(bool force)
        {
            if (force) return true;

            _console.WriteLine("Update checksum anyway? (y/n)");
            var key = _console.ReadKey();
            _console.WriteLine();

            return key == 'y' || key == 'Y';
        }
    }
}

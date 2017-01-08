using System;

namespace ArgSum
{
    public class InputOutput
    {
        public virtual void WriteHelpMessage()
        {
            WriteLine("ArgSum - a tool for updating F1GP/GP2 file checksums");
            WriteLine();
            WriteLine("Usage: ArgSum <file-to-update> [--force] [--pause]");
            WriteLine();
            WriteLine("  <file-to-update>   File to update checksum for");
            WriteLine();
            WriteLine("  --force -f         Force the checksum update even if the file size");
            WriteLine("                     does not match known file sizes.");
            WriteLine();
            WriteLine("  --pause -p         Pauses after updating checksum, useful e.g. when");
            WriteLine("                     running ArgSum as a SendTo-program.");
            WriteLine();
        }

        public virtual void WriteUnrecognizedInputMessage()
        {
            WriteLine("Unrecognized input. Check parameters and try again.");
            WriteForMoreInfoMessage();
        }

        public virtual void WriteFileDoesNotExistMessage(string fileName)
        {
            WriteLine($"The specified file {fileName} does not exist.");
        }

        public virtual void WriteFileArgumentMissingMessage()
        {
            WriteLine("You must specify a file to update!");
        }

        private void WriteForMoreInfoMessage()
        {
            WriteLine("For more info, run ArgSum with the --help argument.");
        }

        public virtual void WriteIdentifiedFileMessage(string description)
        {
            WriteLine($"This looks like {description}");
        }

        public virtual void WriteProbableFileMessage(string description)
        {
            WriteLine($"Not exactly certain but looks like {description}");
        }

        public virtual void WriteUnknownFileMessage()
        {
            WriteLine("This is not a file that ArgSum recognizes.");
        }

        public void WriteDidNotProceedWithUnknownFileMessage()
        {
            WriteLine("Did not proceed with updating unknown file!");
        }

        public void WriteSuccessMessage()
        {
            WriteLine("Checksum updated successfully.");
        }

        public virtual void WriteLine(string value = "")
        {
            Console.WriteLine(value);
        }

        public virtual char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public virtual void WritePauseMessage()
        {
            WriteLine("Press any key to continue.");
            ReadKey();
        }
    }
}

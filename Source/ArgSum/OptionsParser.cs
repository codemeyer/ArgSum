using System.Collections.Generic;
using System.Linq;

namespace ArgSum
{
    public static class OptionsParser
    {
        public static Options Parse(IList<string> args)
        {
            var options = new Options();

            if (args.Count == 0 || args.Any(a => a.Equals("--help")))
            {
                options.DisplayHelp = true;
                return options;
            }

            if (args.Any(a => a.Equals("--force") || a.Equals("-f")))
            {
                options.Force = true;
                args.Remove("--force");
                args.Remove("-f");
            }

            if (args.Any(a => a.Equals("--pause") || a.Equals("-p")))
            {
                options.Pause = true;
                args.Remove("--pause");
                args.Remove("-p");
            }

            if (args.Count == 1)
            {
                options.FileName = args[0];
            }
            else
            {
                options.UnrecognizedInput = true;
            }

            return options;
        }
    }
}

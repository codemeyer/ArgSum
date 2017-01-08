using System;
using System.Linq;

namespace ArgSum
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = OptionsParser.Parse(args.ToList());

            var runner = new Runner(new ChecksumCalculator(), new FileSystem(), new InputOutput());
            runner.Run(options);
        }
    }
}

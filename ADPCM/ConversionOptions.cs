using System;
using System.IO;

namespace ADPCM
{
    class ConversionOptions
    {
        private const int DefaultFrequency = 22050;
        private const string DefaultExtension = "P04";

        public int Frequency { get; private set; }
        public string Extension { get; private set; }
        public string Source { get; }
        public string Destination { get; private set; }
        public bool SourceIsDirectory { get; }

        public ConversionOptions(string source)
        {
            Source = source;
            SourceIsDirectory = !Path.HasExtension(Source);
        }

        public static ConversionOptions FromCommandLineArgs(string[] args)
        {
            return new ConversionOptions(args[0])
            {
                Frequency = args.Length > 1 ? int.Parse(args[1]) : DefaultFrequency,
                Extension = args.Length > 2 ? args[2] : DefaultExtension,
                Destination = args.Length > 3 ? args[3] : args[0]
            };
        }
    }
}

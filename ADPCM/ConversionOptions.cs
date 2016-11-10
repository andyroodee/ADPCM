namespace ADPCM
{
    class ConversionOptions
    {
        private const int DefaultFrequency = 22050;
        private const string DefaultExtension = "P04";

        public int Frequency { get; private set; }
        public string Extension { get; private set; }
        public string SourceFolder { get; private set; }
        public string DestinationFolder { get; private set; }

        public static ConversionOptions FromCommandLineArgs(string[] args)
        {
            return new ConversionOptions
            {
                SourceFolder = args[0],
                Frequency = args.Length > 1 ? int.Parse(args[1]) : DefaultFrequency,
                Extension = args.Length > 2 ? args[2] : DefaultExtension,
                DestinationFolder = args.Length > 3 ? args[3] : args[0]
            };
        }
    }
}

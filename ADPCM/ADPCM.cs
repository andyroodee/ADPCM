using System;

namespace ADPCM
{
    class ADPCM
    {
        static void Main(string[] args)
        {
            try
            {
                ConversionOptions options = ConversionOptions.FromCommandLineArgs(args);
                Converter converter = new Converter(options);
                converter.Run();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("usage: ADPCM.exe sourceFolder [frequency] [sourceFileExtension] [destinationFolder]");
            }
        }
    }
}

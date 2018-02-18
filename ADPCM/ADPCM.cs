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
            catch (Exception e)
            {
                Console.Error.WriteLine("usage: ADPCM.exe source [frequency] [sourceFileExtension] [destination]");
                Console.WriteLine(e.Message);
            }
        }
    }
}

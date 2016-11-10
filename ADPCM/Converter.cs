using System.IO;

namespace ADPCM
{
    class Converter
    {
        private ConversionOptions options;

        public Converter(ConversionOptions options)
        {
            this.options = options;
        }

        public void Run()
        {
            foreach (string fileName in Directory.GetFiles(options.SourceFolder, "*." + options.Extension))
            {
                ConvertToWav(fileName);
            }
        }

        private void ConvertToWav(string fileName)
        {
            // Load the source ADPCM file
            ADPCMFile adpcm = new ADPCMFile(fileName);

            // Make sure the destination path exists
            string destinationFileName = GetFullOutputFileName(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationFileName));

            // Create the converted wav file
            WavFile wav = new WavFile(destinationFileName);
            wav.SetData(options.Frequency, adpcm.Data);
            wav.Save();
        }

        private string GetFullOutputFileName(string sourcePath)
        {
            return options.DestinationFolder + Path.DirectorySeparatorChar + 
                Path.GetFileNameWithoutExtension(sourcePath) + WavFile.Extension;
        }
    }
}

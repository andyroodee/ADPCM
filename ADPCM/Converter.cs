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
            if (options.SourceIsDirectory)
            {
                foreach (string fileName in Directory.GetFiles(options.Source, "*." + options.Extension))
                {
                    ConvertToWav(fileName);
                }
            }
            else
            {
                ConvertToWav(options.Source);
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
            return options.Destination + Path.DirectorySeparatorChar + 
                Path.GetFileNameWithoutExtension(sourcePath) + WavFile.Extension;
        }
    }
}

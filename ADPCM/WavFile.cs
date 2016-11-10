using System.IO;

namespace ADPCM
{
    class WavFile
    {
        public static readonly string Extension = ".wav";
        
        public string FileName { get; private set; }

        public WavFile(string fileName)
        {
            FileName = fileName;
        }

        public void SetData(int frequency, short[] data)
        {
            this.data = data;
            header = new WavHeader(frequency, data.Length * 2);
        }

        public void Save()
        {
            using (var writer = new BinaryWriter(File.Open(FileName, FileMode.Create)))
            {
                header.WriteTo(writer);
                
                foreach (short val in data)
                {
                    writer.Write(val);
                }
            }
        }
        
        private WavHeader header;
        private short[] data;
    }
}

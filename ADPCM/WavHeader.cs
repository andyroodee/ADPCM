using System.Text;
using System.IO;

namespace ADPCM
{
    class WavHeader
    {
        private static readonly byte[] RiffString = Encoding.ASCII.GetBytes("RIFF");       
        private static readonly byte[] WaveString = Encoding.ASCII.GetBytes("WAVEfmt ");
        private static readonly byte[] DataString = Encoding.ASCII.GetBytes("data");
        private const int FormatDataSize = 16;
        private const short Format = 1;
        private const short Bits = 16;
        private const short Channels = 2;
        private const short BlockSize = 4;
        private const byte HeaderSize = 36;
        
        private int frequency;
        private int bytesPerSecond;
        private int dataSize;
        private int totalSize;
        
        public WavHeader(int frequency, int dataSize)
        {
            this.frequency = frequency;
            bytesPerSecond = frequency * BlockSize;
            this.dataSize = dataSize;
            totalSize = dataSize + HeaderSize;
        }

        public void WriteTo(BinaryWriter writer)
        {
            writer.Write(RiffString);
            writer.Write(totalSize);
            writer.Write(WaveString);
            writer.Write(FormatDataSize);
            writer.Write(Format);
            writer.Write(Channels);
            writer.Write(frequency);
            writer.Write(bytesPerSecond);
            writer.Write(BlockSize);
            writer.Write(Bits);
            writer.Write(DataString);
            writer.Write(dataSize);
        }
    }
}

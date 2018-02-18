using System.IO;
using System.Collections.Generic;

namespace ADPCM
{
    class ADPCMFile
    {
        public string FileName { get; private set; }

        private const int MinWaveFormValue = -32768;
        private const int MaxWaveFormValue = 32767;
        private const int MinStep = 0x7f;
        private const int MaxStep = 0x6000;
        private const int ChunkSize = 8192;
        private const int NumOfChannels = 2;

        public short[] Data { get; private set; }

        private static readonly int[] DiffLookup = 
        {
            1, 3, 5, 7, 9, 11, 13, 15,
            -1, -3, -5, -7, -9, -11, -13, -15,
        };

        private static readonly int[] IndexScale = 
        {
            0x0e6, 0x0e6, 0x0e6, 0x0e6, 0x133, 0x199, 0x200, 0x266,
            0x0e6, 0x0e6, 0x0e6, 0x0e6, 0x133, 0x199, 0x200, 0x266
        };

        public ADPCMFile(string fileName)
        {
            FileName = fileName;
            Process();
        }
        
        private int Clamp(int val, int min, int max)
        {
            if (val < min) { return min; }
            if (val > max) { return max; }
            return val;
        }
        
        private int GetNextWaveFormValue(int currentWaveFormValue, int step, byte sample)
        {
            int waveFormValue = currentWaveFormValue + (step * DiffLookup[sample]) / 8;
            return Clamp(waveFormValue, MinWaveFormValue, MaxWaveFormValue);
        }

        private int GetNextStepValue(int currentStep, byte sample)
        {
            int step = (currentStep * IndexScale[sample & 7]) >> 8;
            return Clamp(step, MinStep, MaxStep);
        }
        
        private short ConvertSampleToWave(byte sample, ref int waveFormValue, ref int step)
        {
            waveFormValue = GetNextWaveFormValue(waveFormValue, step, sample);
            step = GetNextStepValue(step, sample);
            return (short)waveFormValue;
        }

        private IEnumerable<short> GetWaveValues(byte[] rawData, int startOffset)
        {
            int waveFormValue = 0;
            int step = 0x7f;
            int offset = startOffset;
            while (offset < rawData.Length)            
            {
                byte sample = (byte)(rawData[offset] & 15);
                yield return ConvertSampleToWave(sample, ref waveFormValue, ref step);

                sample = (byte)((rawData[offset] >> 4) & 15);
                yield return ConvertSampleToWave(sample, ref waveFormValue, ref step);

                offset++;

                // If we hit a ChunkSize boundary, skip ahead to the next chunk
                // that's in our channel
                if ((offset % ChunkSize) == 0)
                {
                    offset += ChunkSize;
                }
            }
        }
        
        private void Process()
        {
            byte[] rawData = File.ReadAllBytes(FileName);
            Data = new short[rawData.Length * 2];
            for (int i = 0; i < NumOfChannels; i++)
            {
                int destIndex = i;
                foreach (short val in GetWaveValues(rawData, i * ChunkSize))
                {
                    Data[destIndex] = val;
                    destIndex += 2;
                }
            }
        }
    }
}

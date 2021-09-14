using System;
using System.IO;
using OodleCompressor.Oodle.Utils;

namespace OodleCompressor.Oodle
{
    public static class Oodle
    {
        public static void Compress(string decompressedFilePath, string outputPath)
        {
            uint @uint; // Needs to be outside so it always has a value
            try
            {
                @uint = (uint) OodleStream.OodleLZ_Compress(OodleFormat.Kraken, File.ReadAllBytes(decompressedFilePath), // Get decompressed buffer
                    File.ReadAllBytes(decompressedFilePath).Length, // Get decompressed length
                    new byte[(int) (uint) File.ReadAllBytes(decompressedFilePath).Length + 274U *
                        (((uint) File.ReadAllBytes(decompressedFilePath).Length + 262143U) / 262144U)], // Get compressed size
                    OodleCompressionLevel.Level5, 0U, 0U, 0U, 0);
            }
            catch (AccessViolationException)
            {
                @uint = 64U; // Just in case there is protected memory
            }
            
            File.WriteAllBytes(outputPath, OodleStream.OodleCompress(File.ReadAllBytes(decompressedFilePath), File.ReadAllBytes(decompressedFilePath).Length,
                OodleFormat.Kraken, OodleCompressionLevel.Level5, @uint)); // Writing the data
        }
    }
}
using System;

namespace OodleTools
{
    public class Oodle : Imports
    {
        /// <summary>
        /// Attempts to compress a byte[] using Oodle.
        /// </summary>
        /// <param name="data">byte[]: The decompressed data you want to compress</param>
        /// <param name="compressedData">out byte[]: The compressed data Oodle returns</param>
        /// <returns>bool: True if the compression was a success, false if it was a failure</returns>
        public static bool TryCompress(byte[] data, out byte[] compressedData)
        {
            try
            {
                compressedData = Compress(data);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                compressedData = new byte[] { };
                return false;
            }
        }

        /// <summary>
        /// Compresses a byte[] using Oodle.
        /// </summary>
        /// <param name="data">byte[]: The decompressed data you want to compress</param>
        /// <returns>byte[]: The compressed data</returns>
        public static byte[] Compress(byte[] data)
        {
            var maxSize = GetCompressedBounds((uint)data.Length);
            var compressedData = new byte[maxSize];
            
            var compressedSize = Compress(data, (uint)data.Length, ref compressedData,
                maxSize, OodleFormat.Kraken, OodleCompressionLevel.Optimal5);
            
            byte[] result = new byte[compressedSize];
            Buffer.BlockCopy(compressedData, 0, result, 0, (int)compressedSize);

            return result;
        }
        
        /// <summary>
        /// Attempts to decompress a byte[] using Oodle.
        /// </summary>
        /// <param name="data">byte[]: The compressed data you want to decompress</param>
        /// <param name="decompressedSize">int: The expected size of the decompressed data.</param>
        /// <param name="decompressedData">out byte[]: The decompressed data</param>
        /// <returns>bool: True if the decompression was a success, false if it was a failure</returns>
        public static bool TryDecompress(byte[] data, int decompressedSize, out byte[] decompressedData)
        {
            try
            {
                decompressedData = Decompress(data, decompressedSize);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                decompressedData = new byte[] { };
                return false;
            }
        }

        /// <summary>
        /// Decompresses a byte[] using Oodle.
        /// </summary>
        /// <param name="data">byte[]: The compressed data</param>
        /// <param name="decompressedSize">int: The expected size of the decompressed data</param>
        /// <returns>byte[]: The decompressed data</returns>
        /// <exception cref="Exception">Gets thrown when "decompressedSize" doesn't match with what Oodle returns</exception>
        public static byte[] Decompress(byte[] data, int decompressedSize)
        {
            byte[] decompressedData = new byte[decompressedSize];
            var verificationSize = Decompress(data, (uint)data.Length, 
                ref decompressedData, (uint)decompressedSize);
            
            if (verificationSize != decompressedSize)
                throw new Exception("Decompression failed. Verification size does not match given size.");
            
            return decompressedData;
        }

        private static uint Compress(byte[] buffer, uint bufferSize, ref byte[] OutputBuffer, uint OutputBufferSize,
            OodleFormat format, OodleCompressionLevel level)
        {
            if (buffer.Length > 0 && bufferSize > 0 && OutputBuffer.Length > 0 && OutputBufferSize > 0)
                return (uint)OodleLZ_Compress(format, buffer, bufferSize, OutputBuffer, level, 0, 0, 0);

            return 0;
        }

        private static uint Decompress(byte[] buffer, uint bufferSize, ref byte[] outputBuffer, uint outputBufferSize)
        {
            if (buffer.Length > 0 && bufferSize > 0 && outputBuffer.Length > 0 && outputBufferSize > 0)
                return (uint)OodleLZ_Decompress(buffer, bufferSize, outputBuffer, outputBufferSize, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            return 0;
        }
    }
}
using System;
using System.IO;
using System.Linq;
using OodleTools;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Test();
        }

        private static void Test()
        {
            // Creates a buffer with a length of 0x400000 bytes full of 0x55
            byte[] buffer = Enumerable.Repeat((byte)0x55, 0x400000).ToArray();
            if (Oodle.TryCompress(buffer, out byte[] compressedData)) // Compresses the buffer
            {
                bool isValid = true; // Sets a temporary variable to true to check if data matches

                // You can't just use byte[] == byte[] in a boolean expression, so we'll just do a loop
                byte[] diffData = Oodle.Compress(buffer); // Compresses the buffer a different way
                for (int i = 0; i < compressedData.Length; i++)
                {
                    if (compressedData[i] == diffData[i]) continue;
                    isValid = false; // Sets the variable to false if the data doesn't match
                    break;
                }
                
                Console.WriteLine(isValid // Prints the result of the comparison
                    ? "Test 1 and 2 passed."
                    : "Compression failed: Oodle.TryCompress() and Oodle.Compress() do not match results.");
                
                Console.WriteLine($"Compression Successful {buffer.Length} -> {compressedData.Length}"); // Prints the size of the compressed data
                
                buffer = Enumerable.Repeat((byte)0xFF, 0x400000).ToArray(); // Sets the buffer to 0xFF (so we can check if decompression works)

                diffData = Oodle.Decompress(compressedData, buffer.Length); // Decompresses the data
                if (Oodle.TryDecompress(compressedData, buffer.Length, out buffer)) // Decompresses the data a different way
                {
                    isValid = true; // Sets the temp variable back to true to check if the data matches
                    bool resultDecompress = true; // A second temp variable because we will also check if decompression works
                    
                    // You can't just use byte[] == byte[] in a boolean expression, so we'll just do a loop
                    for (int i = 0; i < diffData.Length; i++)
                    {
                        if (diffData[i] == buffer[i]) continue;
                        resultDecompress = false; // Set the variable to false if the data doesn't match
                        break;
                    }

                    for (int i = 0; i < 0x400000; i++)
                    {
                        if (buffer[i] == 0x55) continue;
                        isValid = false; // Set the variable to false if the data doesn't match the original data from the first line
                        break;
                    }
                    
                    if (resultDecompress && isValid) // Prints the result of the comparisons
                        Console.WriteLine($"Test 3 passed. \nDecompression Successful {compressedData.Length} -> {buffer.Length}");
                    else
                        Console.Write("Test 3 failed. \nDecompression Failed. Invalid result size or verification failed.");
                }
            }
            else
                Console.WriteLine("Compression Failed: Oodle.TryCompress() returned false. Test 1 failed.");
            
        }
    }
}
using System.Linq;
using OodleCompressor.Oodle;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Oodle.Compress(args[0], args[0].Split('.').First() + ".Tamely.uasset");
        }
    }
}
namespace OodleCompressor.Oodle.Utils
{
    public enum ThreadModule : uint
    {
    }

    public enum OodleFormat : uint
    {
        Lzh,
        Lzhlw,
        Lznib,
        Lzb,
        Lzb16,
        Lzblw,
        Lza,
        Lzna,
        Kraken,
        Mermaid,
        BitKnit,
        Selkie,
        Akkorokamui,
        None
    }

    public enum OodleCompressionLevel : ulong
    {
        None,
        Fastest,
        Faster,
        Fast,
        Normal,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    public enum CompressionType : uint // Used for decompression so not needed here, unless someone wants to add it
    {
        Unknown,
        Oodle,
        Zlib
    }
}
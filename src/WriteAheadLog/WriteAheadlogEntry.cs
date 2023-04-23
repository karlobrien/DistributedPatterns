namespace WriteAheadLog
{
    public class WriteAheadlogEntry
    {
        public byte[] Data { get; }
        public long Timestamp { get; }
        public ulong Index { get; }
        public byte[] Key { get; }

        public WriteAheadlogEntry(byte[] key, byte[] data, ulong index)
        {
            Key = key;
            Data = data;
            Index = index;
            Timestamp = DateTime.UtcNow.Ticks;
        }
    }

    public record LogConfig(string ConfigLocation, string Name);

}

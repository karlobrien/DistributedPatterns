namespace WriteAheadLog
{
    public class WriteAheadlogEntry
    {
        public byte[] Data { get; }
        public long Timestamp { get; }
        public long Index { get; }
        public byte[] Key { get; }

        public WriteAheadlogEntry(byte[] key, byte[] data, long index)
        {
            Key = key;
            Data = data;
            Index = index;
            Timestamp = DateTime.UtcNow.Ticks;
        }
    }
}

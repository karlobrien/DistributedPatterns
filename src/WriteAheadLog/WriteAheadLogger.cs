using System.Text;
using System.Text.Json;
using WriteAheadLog;

namespace WriteAheadLog
{
    /// <summary>
    /// Every value is written to a separate file
    /// the key store can be rebuilt from replaying the files
    /// </summary>
    public class WriteAheadLogger
    {
        private readonly LogConfig _config;

        public WriteAheadLogger(LogConfig config)
        {
            _config = config;
        }

        public void Write(string key, string value, ulong sequence)
        {
            var fileName = $"{_config.Name}.{sequence}.log";
            var entryFile = Path.Combine(_config.ConfigLocation, fileName);
            WriteAheadlogEntry wle = new WriteAheadlogEntry(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value), sequence);
            StreamWrite(wle, entryFile);
        }

        public static void StreamWrite(WriteAheadlogEntry obj, string fileName)
        {
            using var fileStream = File.Create(fileName);
            using var utf8JsonWriter = new Utf8JsonWriter(fileStream);
            JsonSerializerOptions options = new JsonSerializerOptions();
            JsonSerializer.Serialize(utf8JsonWriter, obj, options);
        }
    }
}

public class BasicWaLogger
{
    private readonly LogConfig _config;

    public BasicWaLogger(LogConfig config)
    {
        _config = config;
    }

    public void Write(string key, string value, ulong sequence)
    {
        var fileName = $"{_config.Name}.{sequence}.log";
        var entryFile = Path.Combine(_config.ConfigLocation, fileName);
        WriteAheadlogEntry wle = new WriteAheadlogEntry(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value), sequence);
        StreamWrite(wle, entryFile);
    }

    public static void StreamWrite(WriteAheadlogEntry obj, string fileName)
    {
        using var fileStream = File.Create(fileName);
        using var utf8JsonWriter = new Utf8JsonWriter(fileStream);
        JsonSerializerOptions options = new JsonSerializerOptions();
        JsonSerializer.Serialize(utf8JsonWriter, obj, options);
    }

    public PriorityQueue<WriteAheadlogEntry, ulong> Recover()
    {
        var priorityQueue = new PriorityQueue<WriteAheadlogEntry, ulong>();
        foreach (var item in Directory.EnumerateFiles(_config.ConfigLocation))
        {
            using FileStream openStream = File.OpenRead(item);
            var raw = JsonSerializer.Deserialize<WriteAheadlogEntry>(openStream);
            //order by the sequence
            if (raw != null)
              priorityQueue.Enqueue(raw, raw.Index);
        }

        return priorityQueue;
    }
}

public class LogConfig
{
    public string ConfigLocation { get; }
    public string Name { get; }

    public LogConfig(string path, string name)
    {
        ConfigLocation = path;
        Name = name;
    }

}
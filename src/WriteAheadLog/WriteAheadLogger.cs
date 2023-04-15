using System.Text;
using System.Text.Json;

namespace WriteAheadLog
{
    /// <summary>
    /// Every value is written to a separate file
    /// the key store can be rebuilt from replaying the files
    /// </summary>
    public class WriteAheadLogger
    {
        private string _name;
        private readonly LogConfig _config;
        private long _sequence;

        public WriteAheadLogger(LogConfig config)
        {
            _name = config.Name;
            _config = config;
            //load or set sequence
            _sequence = 1;
        }

        public void Write(string key, string value)
        {
            var fileName = $"{_name}.{_sequence}.log";
            var entryFile = Path.Combine(_config.ConfigLocation, fileName);
            WriteAheadlogEntry wle = new WriteAheadlogEntry(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value), _sequence);
            StreamWrite(wle, entryFile);
            
            _sequence++;
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
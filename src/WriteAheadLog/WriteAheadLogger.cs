using System.Text;
using System.Text.Json;

namespace WriteAheadLog
{
    public class WriteAheadLogger
    {
        private string _name;
        private long _sequence;

        public WriteAheadLogger(string name)
        {
            _name = name;
            //load or set sequence
            _sequence = 1;
        }

        public void Write(string key, string value)
        {
            var fileName = $"{_name}.{_sequence}.log";
            WriteAheadlogEntry wle = new WriteAheadlogEntry(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value), _sequence);
            StreamWrite(wle, fileName);

            //var jsonString = JsonSerializer.Serialize(wle, new JsonSerializerOptions { WriteIndented = true });
            //byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(wle);
            //using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
            //{
            //    var bytes = Encoding.UTF8.GetBytes(value);
            //    stream.Write(bytes, 0, bytes.Length);
            //}

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

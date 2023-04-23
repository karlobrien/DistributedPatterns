using System.Text;
using System.Text.Json;

namespace WriteAheadLog
{
    public class KeyValueStore
    {
        private Dictionary<string, string> _internalStore = new();
        private readonly BasicWaLogger _logger;
        private ulong _sequence;

        public KeyValueStore(BasicWaLogger logger)
        {
            _logger = logger;
        }

        public string Get(string key)
        {
            return _internalStore[key];
        }

        public void Set(string key, string value)
        {
            _logger.Write(key, value, _sequence);
            _internalStore[key] = value;
        }

        public void Init()
        {
            var recoveryQueue = _logger.Recover();
            while (recoveryQueue.Count > 0)
            {
                var latestItem = recoveryQueue.Dequeue();
                if (latestItem != null)
                {
                    _sequence = latestItem.Index;
                    var key = Encoding.UTF8.GetString(latestItem.Key);
                    var value = Encoding.UTF8.GetString(latestItem.Data);
                    _internalStore[key] = value;
                }
            }
        }
    }
}

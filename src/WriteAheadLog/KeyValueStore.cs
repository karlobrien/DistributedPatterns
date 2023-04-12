namespace WriteAheadLog
{
    public class KeyValueStore
    {
        private Dictionary<string, string> _internalStore = new();
        private readonly WriteAheadLogger _logger;

        public KeyValueStore(WriteAheadLogger logger)
        {
            _logger = logger;
        }

        public string Get(string key)
        {
            return _internalStore[key];
        }

        public void Set(string key, string value)
        {
            _logger.Write(key, value);
            _internalStore[key] = value;
        }

    }
}

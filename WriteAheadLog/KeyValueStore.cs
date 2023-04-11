using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WriteAheadLog
{
    public class KeyValueStore
    {
        private Dictionary<string, string> _internalStore = new Dictionary<string, string>();

        public KeyValueStore()
        {
            
        }

        public string Get(string key)
        {
            return _internalStore[key];
        }

        public void Set(string key, string value)
        {
            _internalStore[key] = value;
            //should append to the log here
        }

        private void Append(string key, string value)
        {
            //get the latest number
            WriteAheadlogEntry logEntry = new WriteAheadlogEntry(Encoding.UTF8.GetBytes(value), 1);
        }
    }

    public class WriteAheadlogEntry
    {
        private readonly byte[] _data;
        private long _timestamp;
        private long _index;
        //type of entry

        public WriteAheadlogEntry(byte[] data, long index)
        {
            _data = data;
            _index = index;
            _timestamp = DateTime.UtcNow.Ticks;
        }
    }
}

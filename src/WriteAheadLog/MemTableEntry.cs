namespace WriteAheadLog;

public readonly struct MemTableEntry
{
    public string Key {get;}
    public string? Value {get;}
    public long TimeStampUtc {get;}
    public bool Deleted {get;}

    public MemTableEntry(string key, string? value, long timeStampUtc, bool deleted=false)
    {
        Key = key;
        Value = value;
        TimeStampUtc = timeStampUtc;
        Deleted = deleted;
    }
}

public class MemTable 
{
    public SortedList<string, MemTableEntry> Entries {get;}
    public ulong Size {get;}

    public MemTable()
    {
        Entries = new SortedList<string, MemTableEntry>();
    }

    public MemTableEntry? Get(string key)
    {
        if (Entries.TryGetValue(key, out MemTableEntry entry))
            return entry;
        return null;
    }

    //set
    public void Set(string key, string value, long timestamp)
    {
        MemTableEntry memTableEntry = new(key, value, timestamp);
        if (Entries.ContainsKey(key))
        {
            Entries[key] = memTableEntry;
        }
        else
        {
            Entries.Add(key,memTableEntry);
        }
    }

    //delete
    public void Delete(string key, long timeStampUtc)
    {
        MemTableEntry memTableEntry = new MemTableEntry(key, null, timeStampUtc, true);
        if (Entries.ContainsKey(key))
            Entries[key] = memTableEntry;
        else
            Entries.Add(key,memTableEntry);
    }

    //all entries
    public IList<MemTableEntry> GetEntries()
    {
        return Entries.Values;
    }

    //size

    //length

}

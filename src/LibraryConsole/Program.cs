// See https://aka.ms/new-console-template for more information
using WriteAheadLog;

Console.WriteLine("Hello, World!");

WriteAheadLogger wle = new WriteAheadLogger("First");
KeyValueStore storev = new KeyValueStore(wle);

storev.Set("Karl", "first");
storev.Set("tom", "first");

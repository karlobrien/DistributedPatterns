// See https://aka.ms/new-console-template for more information
using WriteAheadLog;

Console.WriteLine("Starting the logger");

LogConfig lc = new LogConfig(@"C:\Temp\", "Info.Messages");

WriteAheadLogger wle = new WriteAheadLogger(lc);
KeyValueStore storev = new KeyValueStore(wle);

storev.Set("Karl", "first");
storev.Set("tom", "first");

// See https://aka.ms/new-console-template for more information
using WriteAheadLog;

Console.WriteLine("Starting the logger");

LogConfig lc = new LogConfig(@"C:\Temp\", "Info.Messages");

BasicWaLogger basicWaLogger = new BasicWaLogger(lc);
KeyValueStore kvs = new KeyValueStore(basicWaLogger);

kvs.Init();

var result = kvs.Get("tom");

Console.WriteLine(result);

//WriteAheadLogger wle = new WriteAheadLogger(lc);
//KeyValueStore storev = new KeyValueStore(wle);

//storev.Set("Karl", "first");
//storev.Set("tom", "first");

//PriorityQueue<string, int> test = new PriorityQueue<string, int>();
//Console.WriteLine($"{test.EnsureCapacity(0)}");
//var result = test.EnsureCapacity(100);
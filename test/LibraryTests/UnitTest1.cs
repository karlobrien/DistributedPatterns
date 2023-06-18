using WriteAheadLog;

namespace LibraryTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        MemTable memTable= new ();
        memTable.Set("karl", "test", DateTime.UtcNow.Ticks);

        var result = memTable.Get("karl");
        Assert.True(result.HasValue);

        var item = result.Value;
        Assert.True(item.Value == "test");

    }
}
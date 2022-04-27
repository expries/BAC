namespace BAC.Clocks;

public abstract class AbstractClock
{
    private int _nameCounter;

    private readonly string _name;

    protected AbstractClock(string name)
    {
        _name = name;
    }
    
    protected string GenerateEventName() => $"{_name}{++_nameCounter}";
}
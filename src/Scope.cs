namespace IronBrainFuck;
public class Scope
{
    internal List<char> Memory { get; init; }
    internal Stack<int> Points { get; init; }
    private int _pointer;
    internal int Pointer
    {
        get
        {
            if (_pointer < 0)
            {
                return _pointer = Memory.Count - 1;
            }
            return _pointer;
        }
        set => _pointer = value;
    }
    public Scope(List<char> memory, Stack<int> points)
    {
        Memory = memory;
        Points = points;
    }
}

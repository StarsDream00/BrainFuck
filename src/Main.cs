using System.Text;

namespace IronBrainFuck;

public class BrainFuck
{
    private readonly List<char> _memory = new();
    private readonly Stack<int> _points = new();
    private int _ptr;
    private int Ptr
    {
        get
        {
            if (_ptr < 0)
            {
                return _ptr = _memory.Count - 1;
            }
            return _ptr;
        }
        set => _ptr = value;
    }
    private int _index = default;
    private readonly string _code = default;
    private readonly StringBuilder _output = new();
    private readonly Func<char> _inputFunc = () =>
    {
        ConsoleKeyInfo input = Console.ReadKey();
        return input.KeyChar;
    };
    public BrainFuck(string code)
    {
        _code = code;
    }
    public BrainFuck(string code, Func<char> inputFunc)
    {
        _code = code;
        _inputFunc = inputFunc;
    }
    public string Run()
    {
        for (; _index < _code.Length; ++_index)
        {
            if (_memory.Count <= Ptr)
            {
                _memory.Add(default);
            }
            Process();
        }
        return _output.ToString();
    }
    private void Process()
    {
        switch (_code[_index])
        {
            case '>':
                ++Ptr;
                break;
            case '<':
                --Ptr;
                break;
            case '+':
                ++_memory[Ptr];
                break;
            case '-':
                --_memory[Ptr];
                break;
            case '.':
                _output.Append(Convert.ToChar(_memory[Ptr]));
                break;
            case ',':
                _memory[Ptr] = _inputFunc();
                break;
            case '[':
                if (_memory[Ptr] is not default(char))
                {
                    _points.Push(_index - 1);
                    break;
                }
                for (int deep = 1; deep > 0; ++_index)
                {
                    switch (_code[_index])
                    {
                        case '[':
                            deep++;
                            break;
                        case ']':
                            deep--;
                            break;
                    }
                }
                break;
            case ']':
                int point = _points.Pop();
                if (_memory[Ptr] is not default(char))
                {
                    _index = point;
                }
                break;
        }
    }
}

using System.Text;

namespace IronBrainFuck;

public class BrainFuck
{
    private readonly List<char> _memory = new();
    private readonly Stack<int> _points = new();
    private int _ptr = default;
    private int _index = default;
    private readonly string _code = default;
    private readonly StringBuilder _output = new();
    private Func<char> _inputFunc = () =>
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
            if (_memory.Count <= _ptr)
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
                ++_ptr;
                break;
            case '<':
                --_ptr;
                break;
            case '+':
                ++_memory[_ptr];
                break;
            case '-':
                --_memory[_ptr];
                break;
            case '.':
                _output.Append(Convert.ToChar(_memory[_ptr]));
                break;
            case ',':
                _memory[_ptr] = _inputFunc();
                break;
            case '[':
                if (_memory[_ptr] is not default(char))
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
                if (_memory[_ptr] is not default(char))
                {
                    _index = point;
                }
                break;
        }
    }
}

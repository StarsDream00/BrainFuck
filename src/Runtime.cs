using System.Text;

namespace IronBrainFuck;

public class BFRuntime
{
    private readonly Scope _scope;
    private string _code;
    private readonly Func<char> _inputFunc = () =>
    {
        ConsoleKeyInfo input = Console.ReadKey();
        return input.KeyChar;
    };
    private int _index = default;
    private readonly StringBuilder _output = new();
    public BFRuntime(Scope scope, string code = default)
    {
        _scope = scope;
        _code = code;
    }
    public BFRuntime(Scope scope, Func<char> inputFunc, string code = default)
    {
        _scope = scope;
        _code = code;
        _inputFunc = inputFunc;
    }
    public string Run(string code)
    {
        _code = code;
        return Run();
    }
    public string Run()
    {
        for (; _index < _code.Length; ++_index)
        {
            if (_scope.Memory.Count <= _scope.Pointer)
            {
                _scope.Memory.Add(default);
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
                ++_scope.Pointer;
                break;
            case '<':
                --_scope.Pointer;
                break;
            case '+':
                ++_scope.Memory[_scope.Pointer];
                break;
            case '-':
                --_scope.Memory[_scope.Pointer];
                break;
            case '.':
                _output.Append(Convert.ToChar(_scope.Memory[_scope.Pointer]));
                break;
            case ',':
                _scope.Memory[_scope.Pointer] = _inputFunc();
                break;
            case '[':
                if (_scope.Memory[_scope.Pointer] is not default(char))
                {
                    _scope.Points.Push(_index - 1);
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
                int point = _scope.Points.Pop();
                if (_scope.Memory[_scope.Pointer] is not default(char))
                {
                    _index = point;
                }
                break;
        }
    }
}

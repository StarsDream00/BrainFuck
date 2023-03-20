using IronBrainFuck;

while (true)
{
    Console.Write("> ");
    string input = Console.ReadLine();
    BFRuntime bf = new(new(new(), new()), () =>
    {
        Console.Write(">> ");
        ConsoleKeyInfo input = Console.ReadKey();
        Console.WriteLine();
        return input.KeyChar;
    });
    string result = bf.Run(input);
    if (!string.IsNullOrWhiteSpace(result))
    {
        Console.WriteLine(result);
    }
}

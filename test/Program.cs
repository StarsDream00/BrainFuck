using IronBrainFuck;

while (true)
{
    Console.Write("> ");
    string input = Console.ReadLine();
    BrainFuck bf = new(input, () =>
    {
        Console.Write(">> ");
        ConsoleKeyInfo input = Console.ReadKey();
        Console.WriteLine();
        return input.KeyChar;
    });
    string result = bf.Run();
    if (!string.IsNullOrWhiteSpace(result))
    {
        Console.WriteLine(result);
    }
}

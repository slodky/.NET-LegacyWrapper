using LegacyWrapper.Executor.Core.Transport;

namespace LegacyWrapper.Executor.x86;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting LegacyWrapper.Executor.x86");

        var server = new ZeroMqServer();
        server.Run();
    }
}
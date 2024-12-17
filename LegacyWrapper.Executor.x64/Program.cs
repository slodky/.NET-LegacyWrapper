using LegacyWrapper.Executor.Core.Transport;

namespace LegacyWrapper.Executor.x64;

public class Program
{
    /// <summary>
    /// Main method of the legacy dll wrapper.
    /// </summary>
    /// <param name="args">
    /// The first parameter is expected to be a string.
    /// The Wrapper will use this string to create a named pipe.
    /// </param>
    static void Main(string[] args)
    {
        Console.WriteLine("LegacyWrapper64");
        /*var pipe = new NamedPipeServer();
        pipe.Start();*/

        var server = new ZeroMqServer();
        server.Run();
    }
}
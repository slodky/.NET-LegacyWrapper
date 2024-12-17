namespace LegacyWrapper.Client.ProcessHandling
{
    interface IProcessFactory
    {
        MockableProcess GetProcess(string executableName, string args);
    }
}

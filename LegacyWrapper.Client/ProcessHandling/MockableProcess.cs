using System.Diagnostics;

namespace LegacyWrapper.Client.ProcessHandling
{
    /// <summary>
    /// This class is a wrapper for System.Diagnostics.Process for mocking purposes.
    /// </summary>
    internal class MockableProcess : Process
    {
        public new virtual bool Start()
        {
            return base.Start();
        }
    }
}

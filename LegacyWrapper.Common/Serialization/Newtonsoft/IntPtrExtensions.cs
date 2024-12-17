using System;

namespace LegacyWrapper.Common.Serialization;

public static class IntPtrExtensions
{
    public static bool TryParse(string s, out IntPtr result)
    {
        result = IntPtr.Zero;

        if (long.TryParse(s, out long longResult))
        {
            result = new IntPtr(longResult);
            return true;
        }

        return false;
    }
}
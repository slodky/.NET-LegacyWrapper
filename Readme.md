# LegacyWrapper

## About

LegacyWrapper uses a wrapper process to call dlls from a process of the opposing architecture (X86 or X64).

Since you can't load a dll of another architecture directly, the wrapper utilizes ZeroMQ networking library to abstract the call. You won't notice this though, because all the magic is hidden behind a single static method.

## NuGet Packages

Coming...

## Usage

If you want to compile the LegacyWrapper yourself, make sure to place the wrapper executable `LegacyWrapper.Executor.x86.exe`/`LegacyWrapper.Executor.x64.exe` in your directory.

```csharp
// Define a proxy interface with matching method names and signatures
// The interface must be derived from IDisposable!
[LegacyDllImport("User32.dll")]
public interface IUser32Dll : IDisposable
{
    [LegacyDllMethod(CallingConvention = CallingConvention.Winapi)]
    int GetSystemMetrics(int nIndex);
}

// Create configuration
IWrapperConfig configuration = WrapperConfigBuilder.Create()
        .TargetArchitecture(TargetArchitecture.X86)
        .Build();

// Create new Wrapper client providing the proxy interface
// Remember to ensure a call to the Dispose()-Method!
using (var client = WrapperProxyFactory<IUser32Dll>.GetInstance(configuration))
{
    // Make calls - it's that simple!
    int x = client.GetSystemMetrics(0);
    int y = client.GetSystemMetrics(1);
}
```

Please note that loading a 64bit dll will only work on 64bit operating systems.

#### Reference params

If your parameter is reference type you have to add `DontPassRef` attribute

```csharp
[LegacyDllMethod]
uint GetReaderCaps(IntPtr reader, uint nRdFeat, [DontPassRef] ref byte[] pData);
```

## Further reading

View [this blog post](https://codefoundry.de/programming/2015/09/28/legacy-wrapper-invoking-an-unmanaged-32bit-library-out-of-a-64bit-process.html) to obtain a basic understanding of how the library works internally. 

* [There is also a blog post about the dynamic method call feature in LegacyWrapper 3.0](https://codefoundry.de/programming/2019/02/03/legacywrapper-3-0-released.html).
* [There is also a blog post about the new 64bit feature in LegacyWrapper 2.1](https://codefoundry.de/programming/2017/08/20/legacywrapper-2-1-is-out.html).

## Changes between .NET Framework

- `BinaryFormatter` is obsolete so it was changed to `System.Text.Json`
- The library is split into 
    - `Client` .NET Standard library that can be easily referenced in your app
    - `Executor` nuget packages for each platform that includes targeted executable containing trimmed .NET runtime for maximum compatibility
- Trimming is impossible with standard serialization so Source Generation was used
- Due to Source Generation serialization only simple types are possible to pass


## Contributing

Before any contributions can be made I need to clean up the solution first.
It was proof of concept and needed big architecture change.

## License

Copyright (c) 2025, Tomasz Słodyczka. (MIT License)
Based on fantastic work by Franz Wimmer.

See LICENSE for more info.

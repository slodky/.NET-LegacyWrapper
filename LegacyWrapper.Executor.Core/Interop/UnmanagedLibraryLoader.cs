using System;
using System.Reflection;
using System.Reflection.Emit;
using LegacyWrapper.Common.ErrorHandling;
using LegacyWrapper.Common.Serialization;

//using PommaLabs.Thrower;

namespace LegacyWrapper.Executor.Core.Interop
{
    internal static class UnmanagedLibraryLoader
    {
        private const string AssemblyName = "LegacyWrapper";
        private const string ModuleName = "LegacyWrapper";
        private const string TypeName = "LegacyWrapper.WrapperType";

        public static CallResult InvokeUnmanagedFunction(CallData callData)
        {
            Type dllHandle = CreateTypeBuilder(callData);
            MethodInfo methodInfo = dllHandle.GetMethod(callData.ProcedureName);

            if (methodInfo == null)
            {
                throw new LegacyWrapperException(
                    $"Requested method {callData.ProcedureName} was not found in unmanaged DLL.");
            }

            var result = methodInfo.Invoke(null, callData.Parameters);

            return new CallResult()
            {
                Result = result,
                Parameters = callData.Parameters
            };
        }

        private static Type CreateTypeBuilder(CallData callData)
        {
            AssemblyName asmName = new AssemblyName(AssemblyName);
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(ModuleName);

            var typeBuilder = moduleBuilder.DefineType(TypeName, TypeAttributes.Class | TypeAttributes.Public);

            var pinvokeBuilder = typeBuilder.DefinePInvokeMethod(
                name: callData.ProcedureName,
                dllName: callData.LibraryName,
                attributes: MethodAttributes.Static | MethodAttributes.Public | MethodAttributes.PinvokeImpl,
                callingConvention: CallingConventions.Standard,
                returnType: callData.ReturnType,
                parameterTypes: callData.ParameterTypes,
                nativeCallConv: callData.CallingConvention,
                nativeCharSet: callData.CharSet);
            
            pinvokeBuilder.SetImplementationFlags(pinvokeBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);

            return typeBuilder.CreateType();
        }
    }
}

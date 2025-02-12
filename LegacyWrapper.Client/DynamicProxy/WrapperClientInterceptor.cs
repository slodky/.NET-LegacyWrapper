using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using LegacyWrapper.Client.Attributes;
using LegacyWrapper.Client.Client;
using LegacyWrapper.Client.Configuration;
using LegacyWrapper.Common.ErrorHandling;
using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;
using PommaLabs.Thrower;

namespace LegacyWrapper.Client.DynamicProxy
{
    /// <summary>
    /// Interceptor to receive calls to a proxy generated from an interface.
    /// </summary>
    internal class WrapperClientInterceptor : IInterceptor, IDisposable
    {
        private bool _isDisposed = false;

        private readonly WrapperInvoker _wrapperClient;
        private readonly Type _interfaceType;
        private readonly ILibraryNameProvider _libraryNameProvider;

        public WrapperClientInterceptor(Type interfaceType, WrapperInvoker wrapperClient, ILibraryNameProvider libraryNameProvider)
        {
            Raise.ArgumentNullException.IfIsNull(interfaceType, nameof(interfaceType));
            Raise.ArgumentNullException.IfIsNull(wrapperClient, nameof(wrapperClient));
            Raise.ArgumentNullException.IfIsNull(libraryNameProvider, nameof(libraryNameProvider));

            _interfaceType = interfaceType;
            _wrapperClient = wrapperClient;
            _libraryNameProvider = libraryNameProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            Raise.ObjectDisposedException.If(_isDisposed, nameof(WrapperClientInterceptor));

            // Early out if it's a call to Dispose()
            if (invocation.Method.Name == nameof(IDisposable.Dispose))
            {
                Dispose();
                return;
            }

            Type[] parameterTypes = GetParameterTypesFromInvocation(invocation);
            Type returnType = invocation.Method.ReturnType;

            LegacyDllImportAttribute dllImportAttribute = GetLegacyAttribute<LegacyDllImportAttribute>(_interfaceType);
            LegacyDllMethodAttribute dllMethodAttribute = GetLegacyAttribute<LegacyDllMethodAttribute>(invocation.Method);

            string libraryName = _libraryNameProvider.GetLibraryName(dllImportAttribute);

            var callData = new CallData
            {
                LibraryName = libraryName,
                ProcedureName = invocation.Method.Name,
                Parameters = invocation.Arguments,
                ParameterTypes = parameterTypes,
                ReturnType = returnType,
                CallingConvention = dllMethodAttribute.CallingConvention,
                CharSet = dllMethodAttribute.CharSet,
            };
            
            var result = (CallResult) _wrapperClient.InvokeInternal(callData);
            CopyParameters(result, invocation.Arguments);

            var resultWithCorrectType = result.GetProperResult(returnType);

            invocation.ReturnValue = resultWithCorrectType;
        }
        
        private void CopyParameters(CallResult callResult, object[] arguments)
        {
            string errorMessage = "Returned parameters differ in length from passed parameters";
            Raise.InvalidDataException.If(arguments.Length != callResult.Parameters.Length, errorMessage);

            Array.Copy(callResult.Parameters, arguments, callResult.Parameters.Length);
        }

        private Type[] GetParameterTypesFromInvocation(IInvocation invocation)
        {
            return invocation.Method
                .GetParameters()
                //.Select(x => x.ParameterType)
                .Select(x =>
                {
                    if (x.GetCustomAttributes(typeof(DontPassRefAttribute), false).ToArray().Any())
                    {
                        return x.ParameterType.GetElementType();
                    }
                    return x.ParameterType;
                })
                .ToArray();
        }

        private static T GetLegacyAttribute<T>(MemberInfo attributeProvider) where T : Attribute
        {
            var dllImportAttributes = attributeProvider.GetCustomAttributes(typeof(T), false)
                .Cast<T>()
                .ToArray();

            Raise<LegacyWrapperException>.IfNot(dllImportAttributes.Length == 1, $"{attributeProvider.Name} must contain exactly one {typeof(T).Name}");

            return dllImportAttributes[0];
        }

        #region IDisposable-Pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _wrapperClient.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            _isDisposed = true;
        }

        ~WrapperClientInterceptor()
        {
            Dispose(false);
        }
        #endregion
    }
}

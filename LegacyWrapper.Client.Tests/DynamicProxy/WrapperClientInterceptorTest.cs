using System.Reflection;
using Castle.DynamicProxy;
using LegacyWrapper.Client.Attributes;
using LegacyWrapper.Client.Configuration;
using LegacyWrapper.Client.DynamicProxy;
using LegacyWrapper.Client.Transport;
using LegacyWrapper.Common.ErrorHandling;
using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WrapperInvoker = LegacyWrapper.Client.Client.WrapperInvoker;

namespace LegacyWrapper.Client.Tests.DynamicProxy
{
    [LegacyDllImport("TESTLIBRARY")]
    internal interface IMockInterface
    {
        [LegacyDllMethod]
        void TestMethod();
    }

    internal interface IMockInterfaceWithoutAttribute
    {
        [LegacyDllMethod]
        void TestMethod();
    }

    [LegacyDllImport("TESTLIBRARY")]
    internal interface IMockInterfaceWithoutMethodAttribute
    {
        void TestMethod();
    }

    [TestClass]
    public class WrapperClientInterceptorTest
    {
        private const string MockLibraryName = "TESTLIBRARY";

        private static readonly Type MockInterfaceType = typeof(IMockInterface);
        private static readonly Type MockInterfaceTypeWithoutAttribute = typeof(IMockInterfaceWithoutAttribute);
        private static readonly Type MockInterfaceTypeWithoutMethodAttribute = typeof(IMockInterfaceWithoutMethodAttribute);
        private static readonly CallResult ReturnedCallResult = new CallResult();

        private Mock<MethodInfo> _methodInfoMock;
        private Mock<IInvocation> _invocationMock;
        private Mock<IWrapperClient> _pipeConnectorMock;
        private Mock<WrapperInvoker> _wrapperClientMock;
        private Mock<ILibraryNameProvider> _libraryNameProviderMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _methodInfoMock = new Mock<MethodInfo>();
            _methodInfoMock
                .SetupGet(mock => mock.Name)
                .Returns("TestMethod");
        
            _invocationMock = new Mock<IInvocation>();
            _invocationMock
                .SetupGet(mock => mock.Method)
                .Returns(_methodInfoMock.Object);

            _pipeConnectorMock = new Mock<IWrapperClient>();

            _wrapperClientMock = new Mock<WrapperInvoker>(_pipeConnectorMock.Object);
            _wrapperClientMock
                .Setup(mock => mock.InvokeInternal(It.IsAny<CallData>()))
                .Returns(ReturnedCallResult); ;
            _wrapperClientMock
                .As<IDisposable>()
                .Setup(mock => mock.Dispose())
                .Verifiable();

            _libraryNameProviderMock = new Mock<ILibraryNameProvider>();
            _libraryNameProviderMock
                .Setup(mock => mock.GetLibraryName(It.IsAny<LegacyDllImportAttribute>()))
                .Returns(MockLibraryName);
        }

        [TestMethod]
        public void TestInterceptorRetrievesReturnValue()
        {
            _methodInfoMock
                .Setup(mock => mock.GetCustomAttributes(It.IsAny<Type>(), It.IsAny<bool>()))
                .Returns(new object[] { new LegacyDllMethodAttribute() });
            var interceptor = new WrapperClientInterceptor(
                MockInterfaceType, 
                _wrapperClientMock.Object,
                _libraryNameProviderMock.Object);

            interceptor.Intercept(_invocationMock.Object);

            _invocationMock.VerifySet(mock => mock.ReturnValue = ReturnedCallResult, Times.Once);
        }

        [TestMethod, ExpectedException(typeof(LegacyWrapperException))]
        public void TestThrowsOnMissingInterfaceAttribute()
        {
            var interceptor = new WrapperClientInterceptor(
                MockInterfaceTypeWithoutAttribute, 
                _wrapperClientMock.Object, 
                _libraryNameProviderMock.Object);

            interceptor.Intercept(_invocationMock.Object);
        }

        [TestMethod, ExpectedException(typeof(LegacyWrapperException))]
        public void TestThrowsOnMissingMethodAttribute()
        {
            var interceptor = new WrapperClientInterceptor(
                MockInterfaceTypeWithoutMethodAttribute, 
                _wrapperClientMock.Object,
                _libraryNameProviderMock.Object);

            interceptor.Intercept(_invocationMock.Object);
        }

        [TestMethod]
        public void TestInterceptorRetrievesLibraryName()
        {
            _methodInfoMock
                .Setup(mock => mock.GetCustomAttributes(It.IsAny<Type>(), It.IsAny<bool>()))
                .Returns(new object[] { new LegacyDllMethodAttribute() });
            var interceptor = new WrapperClientInterceptor(
                MockInterfaceType, 
                _wrapperClientMock.Object, 
                _libraryNameProviderMock.Object);

            interceptor.Intercept(_invocationMock.Object);

            _libraryNameProviderMock.Verify(mock => mock.GetLibraryName(It.IsAny<LegacyDllImportAttribute>()), Times.Once);
        }

        [TestMethod, ExpectedException(typeof(ObjectDisposedException))]
        public void TestInterceptorCallsDispose()
        {
            var methodInfoMock = new Mock<MethodInfo>();
            methodInfoMock
                .SetupGet(mock => mock.Name)
                .Returns(nameof(WrapperInvoker.Dispose));
            _invocationMock
                .SetupGet(mock => mock.Method)
                .Returns(methodInfoMock.Object);
            var interceptor = new WrapperClientInterceptor(MockInterfaceType, _wrapperClientMock.Object, _libraryNameProviderMock.Object);


            interceptor.Intercept(_invocationMock.Object);

            // Should throw ObjectDisposedException now
            interceptor.Intercept(_invocationMock.Object);
        }
    }
}

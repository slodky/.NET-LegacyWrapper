using LegacyWrapper.Common.Token;

//using Ninject;
//using PommaLabs.Thrower;

namespace LegacyWrapper.Executor.Core.Handler
{
    public static class CallRequestHandlerFactory
    {
        //private static IKernel InjectionKernel { get; }

        static CallRequestHandlerFactory()
        {
            //InjectionKernel = new StandardKernel();

            //InjectionKernel.Bind<IFormatter>().To<BinaryFormatter>();
            //InjectionKernel.Bind<ICallRequestHandler>().To<CallRequestHandler>();
            //InjectionKernel.Bind<IWrapperServer>().To<NamedPipeServer>();
        }

        public static ICallRequestHandler GetInstance(string[] args)
        {
            ExtractTokenFromArgs(args);

            //return InjectionKernel.Get<ICallRequestHandler>();
            return null;
        }

        private static void ExtractTokenFromArgs(string[] args)
        {
            string errorMessage = "The number of arguments passed to this executable has to be exactly 1.";
            //Raise.ArgumentNullException.IfIsNull(args, nameof(args), errorMessage);
            //Raise.ArgumentException.IfNot(args.Length == 1, nameof(args), errorMessage);

            PipeToken token = new PipeToken(args[0]);
            //InjectionKernel.Bind<PipeToken>().ToConstant(token);
        }
    }
}

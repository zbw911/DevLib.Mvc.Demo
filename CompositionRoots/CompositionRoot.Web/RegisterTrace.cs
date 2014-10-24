using Dev.Web.CompositionRootBase;
using Ninject;

namespace CompositionRoot.Web
{
    public class RegisterTrace : IRegister
    {
        public void Register()
        {
            //BeginEndRequestTraceHttpModuleRegister.Do();
        }
         
        public IKernel Kernel { get; set; }
    }
}

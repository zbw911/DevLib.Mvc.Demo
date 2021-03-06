﻿using Dev.Web.CompositionRootBase;
using Ninject;

namespace CompositionRoot.Web
{
    class MsServiceReg : IRegister
    {
        public void Register()
        {
            //如果设置下面的方法，要增加对 Microsoft.Practices.ServiceLocation 的引用 
            //Dev.CommonServiceLocator.NinjectAdapter.AdapterIniter.Do(Kernel);

            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() =>
            {
                return new CommonServiceLocator.NinjectAdapter.NinjectServiceLocator(Kernel);
            });

        }

        public IKernel Kernel { get; set; }
    }
}

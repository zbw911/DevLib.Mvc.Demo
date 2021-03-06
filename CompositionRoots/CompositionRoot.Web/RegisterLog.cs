﻿ 
using Dev.Log;
using Dev.Log.Config;
using Dev.Web.CompositionRootBase;

namespace CompositionRoot.Web
{
    internal class RegisterLog : IRegister
    {
        #region IRegister Members

        public Ninject.IKernel Kernel { get; set; }

        public void Register()
        {
            Setting.SetLogSeverity(LogSeverity.Debug);
            Setting.AttachLog(new Dev.Log.Impl.ObserverLogToLog4net());
        }

        #endregion
    }
}
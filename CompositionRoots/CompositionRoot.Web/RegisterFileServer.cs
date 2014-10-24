using Dev.Framework.FileServer;
using Dev.Framework.FileServer.Config;
using Dev.Framework.FileServer.DocFile;
using Dev.Framework.FileServer.ImageFile;
using Dev.Framework.FileServer.LocalUploaderFileImpl;
using Dev.Framework.FileServer.ShareImpl;
using Dev.Web.CompositionRootBase;
using Ninject;
using Ninject.Web.Common;


namespace CompositionRoot.Web
{
    internal class RegisterFileServer : IRegister
    {
        #region IRegister Members

        public void Register()
        {
            var x = new ReadConfig();
            this.Kernel.Bind<IKey>().To<ShareFileKey>().InRequestScope();
            this.Kernel.Bind<IUploadFile>().To<ShareUploadFile>().InRequestScope();
            //公用方法 
            //var x = new ReadConfig("LocalUploadFile.config");
            //this.Kernel.Bind<IKey>().To<LocalFileKey>().InRequestScope();

            //this.Kernel.Bind<IUploadFile>().To<LocalUploadFile>().InRequestScope();


            //文档类型
            this.Kernel.Bind<IDocFile>().To<DocFileUploader>().InRequestScope();
            //图片类型
            this.Kernel.Bind<IImageFile>().To<ImageUploader>().InRequestScope();
        }

        public IKernel Kernel { get; set; }

        #endregion
    }
}
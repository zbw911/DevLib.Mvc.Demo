using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dev.Comm.Web.Mvc.Pager;
using Dev.Framework.FileServer;
using XX.Data.Models;
using XX.Services;

namespace XX.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly Lazy<ITestUserService> _testuserService;

        public TestController(Lazy<ITestUserService> testuserService)
        {
            _testuserService = testuserService;
        }

        //
        // GET: /Test/
        public ActionResult Index(string key, int page=1)
        {
            const int pageSize = 10;
            int totalCount = 0;
            List<testuser> list = _testuserService.Value.GetTestUserList(key, page, pageSize, out totalCount);
            this.ViewBag.totalCount = totalCount;
            var routeValueDictionary = new System.Web.Routing.RouteValueDictionary();
            if (!string.IsNullOrEmpty(key)) routeValueDictionary.Add("key", key);
            //routeValueDictionary.Add("pagesize", pageSize);
            //if (xxx != null) RouteValueDictionary.Add("xxx", xxx);
            var pager = new Pagination(page, totalCount, pageSize, "Index", "Test", routeValueDictionary, "page");
            ViewBag.pager = pager;
            Dev.Log.Loger.Info("search:testindex_" + page.ToString());
            return View(list);
        }

        public ActionResult Show()
        {
            return View();
        }

        public ActionResult AddTestUser()
                {
            return View();
        }

        [HttpPost]
        public ActionResult AddTestUser(testuser model)
        {
            var uid = this._testuserService.Value.CheckUserName(model.username);
            if (uid != -1) return Content("已存在");
            var result = this._testuserService.Value.AddTestUser(model);
            Dev.Log.Loger.Debug("create:"+model.username);
            if (result)
            {
                return Content("成功");
            }
            else
            {
                return Content("失败");
            }

            //return View();

        }


        public async Task<ActionResult> GetAllASync()
        {
            var users = await this._testuserService.Value.GetAllTestUserASync();

            return View(users);
        }

        //public ActionResult AddTestUserASync()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<ContentResult> AddTestUserASync(testuser model)
        //{
        //    var typeid = await this._testuserService.Value.CheckUserNameAsync(model.username);
        //    //model.Typeid = typeid;
        //    var result = await this._testuserService.Value.AddTestUserAsync(model);

        //    if (result)
        //    {
        //        return Content("成功");
        //    }
        //    else
        //    {
        //        return Content("失败");
        //    }
        //    //return View();
        //}

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase imgFile, string dirName = "image")
        {
            //HttpPostedFileBase imgFilexx = this.HttpContext.Request.Files["imgFile"];
            String fileUrl = "";
            string fileKey = "";
            var docFile = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IDocFile>(); ;
            var imageFile = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IImageFile>(); ;
            var key = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IKey>(); ; ;
            if (dirName == "image")
            {
                fileKey = imageFile.SaveImageFile(imgFile.InputStream, imgFile.FileName);
                fileUrl = key.GetFileUrl(fileKey);
            }
            else
            {
                fileKey = docFile.Save(imgFile.InputStream, imgFile.FileName);
                fileUrl = key.GetFileUrl(fileKey);
            }
            return Content("操作成功:\r\n"+fileKey+"\r\n"+fileUrl);
        }


    }
}
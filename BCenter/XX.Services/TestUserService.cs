using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev.Data.Infras;
using Dev.Data.Infras.Extensions;
using Dev.Framework.Cache;
using XX.Data.Models;

namespace XX.Services
{
    public class TestUserService : ITestUserService
    {
        private readonly ICacheWraper _cacheWraper; //缓存
        private readonly Lazy<IRepository<XX.Data.Models.testuser>> _testuserRepository;

        public TestUserService(ICacheWraper cacheWraper, Lazy<IRepository<testuser>> testuserRepository)
        {
            _cacheWraper = cacheWraper;
            this._testuserRepository = testuserRepository;
        }
        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<XX.Data.Models.testuser> GetTestUserList(string key,int page,int pageSize,out int totalCount)
        {
            var predicate = PredicateBuilder.True<testuser>();
            if (!string.IsNullOrEmpty(key)) predicate = predicate.And(m => m.username.Contains(key));
            //if (createtime!=null) predicate = predicate.And(m => m.createtime>createtime);
            totalCount = this._testuserRepository.Value.GetQuery(predicate).Count();
            return
                this._testuserRepository.Value.GetQuery(predicate)
                    .OrderByDescending(m => m.uid)
                    .Skip((page - 1)*pageSize)
                    .Take(pageSize)
                    .ToList();
        }
        /// <summary>
        /// 缓存读取数据
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<XX.Data.Models.testuser> getTestUserList_Cache(int top)
        {
            Func<List<XX.Data.Models.testuser>> getdata = () => this._testuserRepository.Value.GetQuery(m => m.uid > 0 && m.username != "").OrderBy(m => Guid.NewGuid()).Take(top).ToList();
            var key = "getTestUserList_Cache_" + top.ToString();
            return _cacheWraper.SmartyGetPut<List<XX.Data.Models.testuser>>(key, System.DateTime.Now.AddHours(1), getdata);

            //Func<List<XX.Data.Models.testuser>> getdata = () =>
            //{
            //    return this._testuserRepository.Value.GetQuery(m => m.uid>0 && m.username != "" ).OrderBy(m => Guid.NewGuid()).Take(top).ToList();
            //};
            //var key = "getTestUserList_Cache_" + top.ToString();
            //return _cacheWraper.SmartyGetPut<List<XX.Data.Models.testuser>>(key, System.DateTime.Now.AddHours(1), getdata);
        }
        /// <summary>
        /// 添加testuser
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool AddTestUser(testuser userInfo)
        {
            this._testuserRepository.Value.Add(userInfo);
            int ret=this._testuserRepository.Value.UnitOfWork.SaveChanges();
            return ret > 0;
        }
        /// <summary>
        /// 修改testuser
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool UpdateTestUser(testuser userInfo)
        {
            this._testuserRepository.Value.Update(userInfo);
            int ret = this._testuserRepository.Value.UnitOfWork.SaveChanges();
            return ret > 0;
        }
        /// <summary>
        /// 删除testuser
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DeleteTestUser(int uid)
        {
            this._testuserRepository.Value.Delete(m=>m.uid==uid);
            int ret = this._testuserRepository.Value.UnitOfWork.SaveChanges();
            return ret > 0;
        }

        /// <summary>
        /// 异步添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddTestUserAsync(testuser model)
        {
            this._testuserRepository.Value.Add(model);
            return 1 == await this._testuserRepository.Value.UnitOfWork.SaveChangesAsync();
        }
        /// <summary>
        /// 异步读取用户
        /// </summary>
        /// <returns></returns>
        public Task<List<testuser>> GetAllTestUserASync()
        {
            return _testuserRepository.Value.GetAllAsync();
        }
        /// <summary>
        /// 异步检测用户是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<int> CheckUserNameAsync(string username)
        {
            var one = await _testuserRepository.Value.FindOneAsync(x =>x.username==username);
            //这里逻辑混乱，不用看逻辑
            if (one == null)
            {
                one = new testuser
                {
                    username = username,
                    createtime = DateTime.Now
                };
                this._testuserRepository.Value.Add(one);
                this._testuserRepository.Value.UnitOfWork.SaveChanges();
            }
            return one.uid;
        }



        public int CheckUserName(string username)
        {
            var one =  _testuserRepository.Value.FindOne(x => x.username == username);
            if (one == null) return -1;
            return one.uid;
        }
    }
}

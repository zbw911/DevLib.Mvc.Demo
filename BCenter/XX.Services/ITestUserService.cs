using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XX.Data.Models;

namespace XX.Services
{
    public interface ITestUserService
    {
        List<XX.Data.Models.testuser> GetTestUserList(string key, int page, int pageSize, out int totalCount);

        List<XX.Data.Models.testuser> getTestUserList_Cache(int top);

        bool AddTestUser(testuser userInfo);

        bool UpdateTestUser(testuser userInfo);
        int CheckUserName(string username);

        bool DeleteTestUser(int uid);

        Task<bool> AddTestUserAsync(testuser model);

        Task<List<testuser>> GetAllTestUserASync();

        Task<int> CheckUserNameAsync(string username);
    }
}

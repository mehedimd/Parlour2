using Domain.Enums.AppEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.ContextModel;

namespace Utility.CachingUtility
{
    public class CacheStoreService
    {
        #region Config
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _iMemoryCache;

        public CacheStoreService()
        {
        }

        public CacheStoreService(ApplicationDbContext db, IMemoryCache iMemoryCache)
        {
            _db = db;
            _iMemoryCache = iMemoryCache;
        }

        #endregion

        public void Clear()
        {
            _iMemoryCache.Dispose();
        }

        public T Get<T>(string cacheKey) where T : class
        {
            T returnValue;

            if (!_iMemoryCache.TryGetValue("itemCacheKey", out returnValue))
            {
                return null;
            }

            return (T)returnValue;
        }

        public void Add<T>(string key, T o)
        {
            T cacheEntry;

            if (!_iMemoryCache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = o;

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(1000),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };

                _iMemoryCache.Set(key, cacheEntry, cacheExpiryOptions);
            }

        }

        public List<T> GetSession<T>(string cacheKey) where T : class
        {
            var dataList = new List<T>();

            if (!_iMemoryCache.TryGetValue(cacheKey, out dataList))
            {
                dataList = GetDataFormDb<T>(cacheKey);

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };

                _iMemoryCache.Set(cacheKey, dataList, cacheExpiryOptions);
            }
            else
            {
                _iMemoryCache.TryGetValue(cacheKey, out dataList);
            }

            return dataList;
        }

        public List<T> GetDataFormDb<T>(string cacheListName) where T : class
        {
            var dataList = new List<T>();

            if (cacheListName == CacheEnum.RoleList.ToString())
            {
                dataList = _db.Roles.OrderBy(c => c.Name).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.EmployeeList.ToString())
            {
                dataList = _db.Employees.OrderByDescending(c => c.Name).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.EmpLeaveType.ToString())
            {
                dataList = _db.LeaveTypes.OrderByDescending(c => c.TypeName).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.PrShiftList.ToString())
            {
                dataList = _db.PrShifts.OrderByDescending(c => c.StartTime).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.BranchList.ToString())
            {
                dataList = _db.Branches.OrderByDescending(c => c.BranchName).ToList() as List<T>;
            }
            return dataList;
        }

        public List<T> AddOrUpdate<T>(string cacheListName) where T : class
        {
            var dataList = new List<T>();

            if (cacheListName == CacheEnum.RoleList.ToString())
            {
                dataList = _db.Roles.OrderBy(c => c.Name).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.EmployeeList.ToString())
            {
                dataList = _db.Employees.OrderByDescending(c => c.Name).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.EmpLeaveType.ToString())
            {
                dataList = _db.LeaveTypes.OrderByDescending(c => c.TypeName).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.PrShiftList.ToString())
            {
                dataList = _db.PrShifts.OrderByDescending(c => c.StartTime).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.PrServiceCategoryList.ToString())
            {
                dataList = _db.PrServiceCategories.OrderByDescending(c => c.CategoryName).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.PrCustomerList.ToString())
            {
                dataList = _db.PrCustomers.OrderByDescending(c => c.CustomerName).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.PrServiceInfoList.ToString())
            {
                dataList = _db.PrServiceInfos.OrderByDescending(c => c.ServiceName).ToList() as List<T>;
            }
            else if (cacheListName == CacheEnum.BranchList.ToString())
            {
                dataList = _db.Branches.OrderByDescending(c => c.BranchName).ToList() as List<T>;
            }

            return dataList;
        }
    }
}

using Microsoft.Practices.Unity;
using Pattern.Common.Contract;
using Pattern.Models.Response.Base;
using Pattern.Repositories.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;

namespace Pattern.Services.Implementation.Base
{
    public class ServiceBase
    {
        [Dependency]
        protected IUserProfile UserProfile { get; set; }

        [Dependency]
        protected ISystemSettings Settings { get; set; }

        [Dependency]
        protected IUnitOfWork UnitOfWork { get; set; }


        /// <summary>
        /// Generic function used to collect data for client-side table display along with paging information 
        /// </summary>
        /// <typeparam name="TM">The Type of the ViewModel required for display</typeparam>
        /// <typeparam name="TQ">The Type of the IQueryable entity collection</typeparam>
        /// <param name="requestPage">The current page number received by the incoming PagedItemRequestModel request</param>
        /// <param name="requestPageSize">The number of table rows to be displayed as defined by the PagedItemRequestModel request</param>
        /// <param name="query">Entity collection data</param>
        /// <returns>PagedItemModel data that represents a single page to display</returns>
        public PagedItemModel<TM> Pager<TM, TQ>(int requestPage, int requestPageSize, IQueryable<TQ> query)
        {
            var _totalCount = query.Count();

            var _skip = requestPageSize * requestPage;
            var _take = requestPageSize;
            var _pageData = query.Skip(_skip).Take(_take).ToList();

            // convert entity data fetched via the query in a ViewModel enumerable (the table data to display)
            var items = Mapper.Map<IEnumerable<TM>>(_pageData);

            return new PagedItemModel<TM>
            {
                TotalCount = _totalCount,
                PageSize = requestPageSize,
                BoF = requestPage == 0,
                EoF = _skip + requestPageSize >= _totalCount,
                CurrentPage = requestPage + 1,
                StartPosition = _skip + 1,
                EndPosition = _skip + requestPageSize < _totalCount ? _skip + requestPageSize : _totalCount,
                Items = items
            };
        }


        /// <summary>
        /// Generic function used to collect data for client-side table display along with paging information 
        /// </summary>
        /// <typeparam name="TM">The Type of the ViewModel required for display</typeparam>
        /// <typeparam name="TQ">The Type of the IQueryable entity collection</typeparam>
        /// <param name="requestPage">The current page number received by the incoming PagedItemRequestModel request</param>
        /// <param name="requestPageSize">The number of table rows to be displayed as defined by the PagedItemRequestModel request</param>
        /// <param name="query">Entity collection data</param>
        /// <returns>PagedItemModel data that represents a single page to display</returns>
        public async Task<PagedItemModel<TM>> PagerAsync<TM, TQ>(int requestPage, int requestPageSize, IQueryable<TQ> query)
        {
            var _totalCount = await query.CountAsync();

            var _skip = requestPageSize * requestPage;
            var _take = requestPageSize;
            var _pageData = await query.Skip(_skip).Take(_take).ToListAsync();

            // convert entity data fetched via the query in a ViewModel enumerable (the table data to display)
            var items = Mapper.Map<IEnumerable<TM>>(_pageData);

            return new PagedItemModel<TM>
            {
                TotalCount = _totalCount,
                PageSize = requestPageSize,
                BoF = requestPage == 0,
                EoF = _skip + requestPageSize >= _totalCount,
                CurrentPage = requestPage + 1,
                StartPosition = _skip + 1,
                EndPosition = _skip + requestPageSize < _totalCount ? _skip + requestPageSize : _totalCount,
                Items = items
            };
        }
    }
}

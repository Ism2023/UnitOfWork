using Pattern.Models.Request;
using Pattern.Models.Response;
using Pattern.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pattern.Services.Contract
{
    public interface IWidgetService
    {
        WidgetModel Get(Guid id);
        Task<WidgetModel> GetAsync(Guid id);

        PagedItemModel<WidgetModel> PagedList(PagedItemRequestModel request);
        Task<PagedItemModel<WidgetModel>> PagedListAsync(PagedItemRequestModel request);

        WidgetModel Save(WidgetModel model);
        Task<WidgetModel> SaveAsync(WidgetModel model);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);        
    }
}

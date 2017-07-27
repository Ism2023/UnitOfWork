using AutoMapper;
using Pattern.Services.Contract;
using Pattern.Services.Extension;
using Pattern.Services.Implementation.Base;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pattern.Models.Request;
using Pattern.Models.Response;
using Pattern.DataContext.Entities;
using Pattern.Models.Response.Base;

namespace Pattern.Services.Implementation
{
    public class WidgetService : ServiceBase, IWidgetService
    {
        public WidgetModel Get(Guid id)
        {
            return _Get(id);
        }

        public async Task<WidgetModel> GetAsync(Guid id)
        {
            return await _GetAsync(id);
        }

        public PagedItemModel<WidgetModel> PagedList(PagedItemRequestModel request)
        {
            var _repo = UnitOfWork.Repository<Widget>();
            var _q = _repo.Query()
                .Include(x => x.LU_WidgetType)
                .Filter(x => !x.Deleted.HasValue)
                .Get();

            if (request.Filter != null)
            {
                _q = _q.Where($"{request.Filter.field}.StartsWith(@0)", request.Filter.criteria);
            }

            _q = request.Sort != null ? _q.OrderBy(request.Sort.field + (request.Sort.useAscending ? "" : " descending")) : _q.OrderBy(x => x.Name);
            return Pager<WidgetModel, Widget>(request.Page, request.PageSize, _q);
        }

        public async Task<PagedItemModel<WidgetModel>> PagedListAsync(PagedItemRequestModel request)
        {
            var _repo =UnitOfWork.Repository<Widget>();
            var _q = _repo.Query()
                .Include(x => x.LU_WidgetType)
                .Filter(x => !x.Deleted.HasValue)
                .Get();

            if (request.Filter != null)
            {
                _q = _q.Where($"{request.Filter.field}.StartsWith(@0)", request.Filter.criteria);
            }

            _q = request.Sort != null ? _q.OrderBy(request.Sort.field + (request.Sort.useAscending ? "" : " descending")) : _q.OrderBy(x => x.Name);
            return await PagerAsync<WidgetModel, Widget>(request.Page, request.PageSize, _q);
        }

        public WidgetModel Save(WidgetModel model)
        {
            Widget _input = new Widget();
            Mapper.Map(model, _input);

            var _repo = UnitOfWork.Repository<Widget>();

            var _old = _repo
                .Query()
                .Filter(x => x.GUID == _input.GUID)
                .FirstOrDefault();

            if (_old == null)
            {
                _repo.Insert(_input);
            }
            else
            {
                if (!_input.Equals(_old))
                {
                    _input.CopyTo(_old);
                    _repo.Update(_old);
                }
            }

            UnitOfWork.Save();
            return _Get(_input.GUID);
        }

        public async Task<WidgetModel> SaveAsync(WidgetModel model)
        {
            Widget _input = new Widget();
            Mapper.Map(model, _input);

            var _repo = UnitOfWork.Repository<Widget>();

            var _old = await _repo
                .Query()
                .Filter(x => x.GUID == _input.GUID)
                .FirstOrDefaultAsync();

            if (_old == null)
            {
                _repo.Insert(_input);
            }
            else
            {
                if (!_input.Equals(_old))
                {
                    _input.CopyTo(_old);
                    _repo.Update(_old);
                }
            }

            await UnitOfWork.SaveAsync();
            return await _GetAsync(_input.GUID);
        }

        public void Delete(Guid id)
        {
            var _deleted = UnitOfWork.Repository<Widget>()
                .Query()
                .Filter(x => x.GUID == id)
                .FirstOrDefault();

            if (_deleted != null)
            {
                _deleted.Deleted = DateTime.Now;
                _deleted.DeletedBy = UserProfile.UserName;
                _deleted.DeletedById = UserProfile.UserId;
                UnitOfWork.Save();
            }
            else
                throw new ApplicationException("Nothing to delete on id : " + id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var _deleted = await UnitOfWork.Repository<Widget>()
                .Query()
                .Filter(x => x.GUID == id)
                .FirstOrDefaultAsync();

            if (_deleted != null)
            {
                _deleted.Deleted = DateTime.Now;
                _deleted.DeletedBy = UserProfile.UserName;
                _deleted.DeletedById = UserProfile.UserId;
                await UnitOfWork.SaveAsync();
            }
            else
                throw new ApplicationException("Nothing to delete on id : " + id);
        }


        #region Private

        private WidgetModel _Get(Guid id)
        {
            return UnitOfWork.Repository<Widget>()
                .Query()
                .Filter(x => x.GUID == id)
                .FirstOrDefault<Widget, WidgetModel>();
        }

        private async Task<WidgetModel> _GetAsync(Guid id)
        {
            return await UnitOfWork.Repository<Widget>()
                .Query()
                .Filter(x => x.GUID == id)
                .FirstOrDefaultAsync<Widget, WidgetModel>();
        }

        #endregion
    }
}

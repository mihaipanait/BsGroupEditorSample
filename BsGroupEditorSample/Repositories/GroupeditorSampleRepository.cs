using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BForms.Grid;
using BsGroupEditorSample.Mock;
using BsGroupEditorSample.Models;

namespace BsGroupEditorSample.Repositories
{
    public class GroupEditorSampleRepository : BsBaseGridRepository<MenuItem, SampleGroupRowModel>
    {
        #region Properties and Constructor

        private BFormsContext db;

        public BsGridRepositorySettings<MenuItemSearchModel> Settings
        {
            get
            {
                return settings as BsGridRepositorySettings<MenuItemSearchModel>;
            }
        }

        public GroupEditorSampleRepository(BFormsContext _db)
        {
            db = _db;
        }

        #endregion

        #region Mappers
        public Func<MenuItem, SampleGroupRowModel> MapMenuItem_SampleGroupRowModel = x =>
            new SampleGroupRowModel
            {
                Id = x.Id,
                DisplayName = x.DisplayName
            };
        #endregion

        #region Filter/Order/Map
        public override IQueryable<MenuItem> Query()
        {
            var query = db.MenuItems.AsQueryable();
            return Filter(query);
        }

        public override IOrderedQueryable<MenuItem> OrderQuery(IQueryable<MenuItem> query)
        {
            var orderedQuery = this.orderedQueryBuilder.Order(query, x => x.OrderBy(y => y.Id));
            return orderedQuery;
        }

        public override IEnumerable<SampleGroupRowModel> MapQuery(IQueryable<MenuItem> query)
        {
            return query.Select(MapMenuItem_SampleGroupRowModel);
        }

        public IQueryable<MenuItem> Filter(IQueryable<MenuItem> query)
        {

            var settings = this.Settings;
            //implement filter logic

            return query;
        }

        #endregion


        internal MenuItemSearchModel GetSearchForm()
        {
            return new MenuItemSearchModel();
        }

        internal MenuItemNewModel GetNewForm()
        {
            return new MenuItemNewModel();
        }
    }
}
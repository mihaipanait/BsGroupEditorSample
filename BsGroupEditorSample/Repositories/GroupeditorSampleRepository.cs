using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BForms.Grid;
using BForms.Models;
using BsGroupEditorSample.Mock;
using BsGroupEditorSample.Models;

namespace BsGroupEditorSample.Repositories
{
    public class GroupEditorSettings : BsGridRepositorySettings<MenuItemSearchModel>
    {
        public MenuItemTypes TabId { get; set; }
    }

    public class GroupEditorSampleRepository : BsBaseGridRepository<MenuItem, SampleGroupRowModel>
    {

        #region Properties and Constructor

        private BFormsContext db;

        public GroupEditorSettings Settings
        {
            get
            {
                return settings as GroupEditorSettings;
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
                DisplayNameLocal = x.DisplayNameLocal,
                DisplayNameInternational = x.DisplayNameInternational,
                Link = x.Link,
                Permissions = x.Visibility.ToString(),
                Icon = x.Icon
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

            if (settings != null)
            {
                if(settings.TabId != null)
                    query = query.Where(x => x.MenuItemType == settings.TabId);

                if (!string.IsNullOrEmpty(Settings.QuickSearch))
                {
                    var searched = settings.QuickSearch.ToLower();

                    query = query.Where(x => x.DisplayNameLocal.ToLower().Contains(searched) ||
                                                        x.DisplayNameInternational.ToLower().Contains(searched) ||
                                                        x.Link.ToLower().Contains(searched));
                }
                else if (settings.Search != null)
                {
                }
            }

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
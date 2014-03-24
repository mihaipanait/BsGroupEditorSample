using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using BForms.Grid;
using BForms.Models;
using Menu.Mock;
using Menu.Models;

namespace Menu.Repositories
{
    public class GroupEditorSettings : BsGridRepositorySettings<MenuItemSearchModel>
    {
        public MenuItemTypes TabId { get; set; }
    }

    public class MenuRepository : BsBaseGridRepository<MenuItem, SampleGroupRowModel>
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

        public MenuRepository(BFormsContext _db)
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
                    #region DisplayName
                    if (!string.IsNullOrEmpty(Settings.Search.DisplayName))
                    {
                        var displayName = Settings.Search.DisplayName.ToLower();
                        query = query.Where(x => x.DisplayNameLocal.ToLower().Contains(displayName) ||
                                                 x.DisplayNameInternational.ToLower().Contains(displayName));
                    }
                    #endregion

                    #region Link
                    if (!string.IsNullOrEmpty(Settings.Search.Link))
                    {
                        var link = Settings.Search.Link.ToLower();
                        query = query.Where(x => x.Link.ToLower().Contains(link));
                    }
                    #endregion

                    #region DisplayName
                    if (Settings.Search.Visibility.SelectedValues.HasValue)
                    {
                        var visibility = Settings.Search.Visibility.SelectedValues.Value;
                        query = query.Where(x => x.Visibility == visibility);
                    }
                    #endregion
                }
            }

            return query;
        }

        #endregion


        internal MenuItemSearchModel GetSearchForm()
        {
            return new MenuItemSearchModel();
        }

        internal PageNewModel GetNewPageForm()
        {
            return new PageNewModel();
        }

        internal CustomLinkNewModel GetNewLinkForm()
        {
            return new CustomLinkNewModel();
        }

        internal CategoryNewModel GetNewCategoryForm()
        {
            return new CategoryNewModel();
        }

        public SampleGroupRowModel CreatePage(PageNewModel model)
        {
            var entity = new MenuItem();
            if(model != null)
            {
                entity.DisplayNameInternational = model.DisplayNameInternational;
                entity.DisplayNameLocal = model.DisplayNameLocal;
                entity.MenuItemType = model.MenuItemType;
                entity.Link = model.Link;
                entity.Visibility = model.Visibility.SelectedValues;

                if (model.Icon.SelectedValues.HasValue)
                    entity.Icon = model.Icon.SelectedValues.Value;

                db.MenuItems.Add(entity);
                db.SaveChanges();
            }

            return MapMenuItem_SampleGroupRowModel(entity);
        }
    }
}
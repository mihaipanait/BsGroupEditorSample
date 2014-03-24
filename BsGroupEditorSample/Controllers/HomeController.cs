using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BForms.Editor;
using BForms.Grid;
using BForms.Models;
using BForms.Mvc;
using Menu.Mock;
using Menu.Models;
using Menu.Repositories;
using Menu.Resources;

namespace Menu.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MenuRepository repo;

        public HomeController()
        {
            repo = new MenuRepository(Db);
        }

        public ActionResult Index()
        {
            var bsGridSettings = new GroupEditorSettings
            {
                Page = 1,
                PageSize = 5,
                TabId = MenuItemTypes.Page
            };

            var model = new GroupEditorModel()
            {
                Tab1 = new BsEditorTabModel<MenuGroupRowModel, MenuItemSearchModel, PageNewModel>
                {
                    Grid = repo.ToBsGridViewModel(bsGridSettings),
                    Search = repo.GetSearchForm(),
                    New = repo.GetNewPageForm()
                },

                Group1 = new BsEditorGroupModel<MenuGroupRowModel>
                {
                    Items = new List<MenuGroupRowModel>()
                },

                Group2 = new BsEditorGroupModel<MenuGroupRowModel>
                {
                    Items = new List<MenuGroupRowModel>()
                },

                Group3 = new BsEditorGroupModel<MenuGroupRowModel>
                {
                    Items = new List<MenuGroupRowModel>()
                }
            };

            var viewModel = new GroupEditorViewModel
            {
                Editor = model
            };

            var options = new
            {
                getTabUrl = Url.Action("GetTab"),
                save = Url.Action("Save"),
                advancedSearchUrl = Url.Action("Search"),
                addUrl = Url.Action("NewPage")
            };

            RequireJsOptions.Add("index", options);

            return View(viewModel);
        }

        public BsJsonResult GetTab(GroupEditorSettings settings)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            var html = string.Empty;
            var count = 0;

            try
            {
                html = RenderTab(settings, out count);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                status = BsResponseStatus.ServerError;
            }

            return new BsJsonResult(new
            {
                Count = count,
                Html = html
            }, status, msg);
        }

        public BsJsonResult Search(MenuItemSearchModel model)
        {
            var settings = new GroupEditorSettings
            {
                Search = model,
                TabId = MenuItemTypes.Page
            };
            var count = 0;

            var html = this.RenderTab(settings, out count);

            return new BsJsonResult(new
            {
                Count = count,
                Html = html
            });
        }

        public BsJsonResult NewPage(PageNewModel model)
        {
            var status = BsResponseStatus.Success;
            var row = string.Empty;
            var msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    var rowModel = repo.CreatePage(model);

                    var groupEditorModel = new GroupEditorModel
                    {
                        
                        Tab1 = new BsEditorTabModel<MenuGroupRowModel, MenuItemSearchModel, PageNewModel>
                        {
                            Grid = new BsGridModel<MenuGroupRowModel>
                            {
                                Items = new List<MenuGroupRowModel>
                                {
                                    rowModel
                                }
                            }
                        }
                    };

                    var viewModel = new GroupEditorViewModel()
                    {
                        Editor = groupEditorModel
                    };

                    row = this.BsRenderPartialView("_Editors", viewModel);

                }
                else
                {
                    return new BsJsonResult(
                        new Dictionary<string, object> { { "Errors", ModelState.GetErrors() } },
                        BsResponseStatus.ValidationError);
                }
            }
            catch (Exception ex)
            {
                msg = Resource.ServerError;
                status = BsResponseStatus.ServerError;
            }

            return new BsJsonResult(new
            {
                Row = row
            }, status, msg);
        }

         [NonAction]
        public string RenderTab(GroupEditorSettings settings, out int count)
        {
            var html = string.Empty;
            count = 0;

            var model = new GroupEditorModel();

            switch (settings.TabId)
            {
                case MenuItemTypes.Page:

                    var grid1 = repo.ToBsGridViewModel(settings, out count);

                    model.Tab1 = new BsEditorTabModel<MenuGroupRowModel, MenuItemSearchModel, PageNewModel>
                    {
                        Grid = grid1,
                        Search = repo.GetSearchForm(),
                        New = repo.GetNewPageForm()
                    };
                    break;

                case MenuItemTypes.CustomLink:

                    var grid2 = repo.ToBsGridViewModel(settings, out count);

                    model.Tab2 = new BsEditorTabModel<MenuGroupRowModel, MenuItemSearchModel, CustomLinkNewModel>
                    {
                        Grid = grid2,
                        Search = repo.GetSearchForm(),
                        New = repo.GetNewLinkForm()
                    };
                    break;

                case MenuItemTypes.Category:

                    var grid3 = repo.ToBsGridViewModel(settings, out count);

                    model.Tab3 = new BsEditorTabModel<MenuGroupRowModel, MenuItemSearchModel, CategoryNewModel>
                    {
                        Grid = grid3,
                        Search = repo.GetSearchForm(),
                        New = repo.GetNewCategoryForm()
                    };
                    break;
            }

            var viewModel = new GroupEditorViewModel()
            {
                Editor = model
            };

            html = this.BsRenderPartialView("_Editors", viewModel);

            return html;
        }

        public ActionResult Save()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BForms.Editor;
using BForms.Grid;
using BForms.Models;
using BForms.Mvc;
using BsGroupEditorSample.Mock;
using BsGroupEditorSample.Models;
using BsGroupEditorSample.Repositories;

namespace BsGroupEditorSample.Controllers
{
    public class HomeController : BaseController
    {
        private readonly GroupEditorSampleRepository repo;

        public HomeController()
        {
            repo = new GroupEditorSampleRepository(Db);
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
                Tab1 = new BsEditorTabModel<SampleGroupRowModel, MenuItemSearchModel, PageNewModel>
                {
                    Grid = repo.ToBsGridViewModel(bsGridSettings),
                    Search = repo.GetSearchForm(),
                    New = repo.GetNewForm() as PageNewModel
                },

                Group1 = new BsEditorGroupModel<SampleGroupRowModel>
                {
                    Items = new List<SampleGroupRowModel>()
                },

                Group2 = new BsEditorGroupModel<SampleGroupRowModel>
                {
                    Items = new List<SampleGroupRowModel>()
                },

                Group3 = new BsEditorGroupModel<SampleGroupRowModel>
                {
                    Items = new List<SampleGroupRowModel>()
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
                addUrl = Url.Action("New")
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

        public ActionResult Save()
        {
            throw new NotImplementedException();
        }

        public ActionResult Search()
        {
            throw new NotImplementedException();
        }

        public ActionResult New()
        {
            throw new NotImplementedException();
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

                    model.Tab1 = new BsEditorTabModel<SampleGroupRowModel, MenuItemSearchModel, PageNewModel>
                    {
                        Grid = grid1,
                        Search = repo.GetSearchForm(),
                        New = repo.GetNewForm() as PageNewModel
                    };
                    break;

                case MenuItemTypes.CustomLink:

                    var grid2 = repo.ToBsGridViewModel(settings, out count);

                    model.Tab2 = new BsEditorTabModel<SampleGroupRowModel, MenuItemSearchModel, CustomLinkNewModel>
                    {
                        Grid = grid2,
                        Search = repo.GetSearchForm(),
                        New = repo.GetNewForm() as CustomLinkNewModel
                    };
                    break;

                case MenuItemTypes.Category:

                    var grid3 = repo.ToBsGridViewModel(settings, out count);

                    model.Tab3 = new BsEditorTabModel<SampleGroupRowModel, MenuItemSearchModel, CategoryNewModel>
                    {
                        Grid = grid3,
                        Search = repo.GetSearchForm(),
                        New = repo.GetNewForm() as CategoryNewModel
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
    }
}
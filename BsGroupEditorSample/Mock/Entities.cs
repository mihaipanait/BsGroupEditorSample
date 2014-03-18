using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BForms.Models;

namespace BsGroupEditorSample.Mock
{
    #region Context
    [Serializable]
    public class BFormsContext
    {
        public BFormsContext()
        {
            #region MenuItems
            MenuItems = new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Id = 1,
                            DisplayNameLocal = "Home",
                            DisplayNameInternational = "Home",
                            MenuItemType = MenuItemTypes.Page,
                            Link = "/Home.html",
                            Icon = Glyphicon.Home,
                            Visibility = MenuItemVisibility.Both
                        },
                        new MenuItem()
                        {
                            Id = 2,
                            DisplayNameLocal = "About",
                            DisplayNameInternational = "About",
                            MenuItemType = MenuItemTypes.Page,
                            Link = "/About.html",
                            Icon = Glyphicon.QuestionSign,
                            Visibility = MenuItemVisibility.FrontEnd
                        },
                        new MenuItem()
                        {
                            Id = 3,
                            DisplayNameLocal = "Administration",
                            DisplayNameInternational = "Administration",
                            MenuItemType = MenuItemTypes.Page,
                            Link = "/AdminHome.html",
                            Icon = Glyphicon.Cog,
                            Visibility = MenuItemVisibility.Both
                        },
                        new MenuItem()
                        {
                            Id = 4,
                            DisplayNameLocal = "Site Statistics",
                            DisplayNameInternational = "Site Statistics",
                            MenuItemType = MenuItemTypes.Page,
                            Link = "/Statistics.html",
                            Icon = Glyphicon.Stats,
                            Visibility = MenuItemVisibility.BackEnd
                        },
                        new MenuItem()
                        {
                            Id = 5,
                            DisplayNameLocal = "Users Management",
                            DisplayNameInternational = "Users Management",
                            MenuItemType = MenuItemTypes.Page,
                            Link = "/ManageUsers.html",
                            Icon = Glyphicon.User,
                            Visibility = MenuItemVisibility.BackEnd
                        },
                        new MenuItem()
                        {
                            Id = 6,
                            DisplayNameLocal = "News",
                            DisplayNameInternational = "News",
                            MenuItemType = MenuItemTypes.Category,
                            Link = "/Blog?type=news",
                            Icon = Glyphicon.List,
                            Visibility = MenuItemVisibility.FrontEnd
                        },
                        new MenuItem()
                        {
                            Id = 7,
                            DisplayNameLocal = "Products",
                            DisplayNameInternational = "Products",
                            MenuItemType = MenuItemTypes.Category,
                            Link = "/Blog?type=product",
                            Icon = Glyphicon.Euro,
                            Visibility = MenuItemVisibility.FrontEnd
                        },
                        new MenuItem()
                        {
                            Id = 8,
                            DisplayNameLocal = "Reviews",
                            DisplayNameInternational = "Reviews",
                            MenuItemType = MenuItemTypes.Category,
                            Link = "/Blog?type=review",
                            Icon = Glyphicon.User,
                            Visibility = MenuItemVisibility.FrontEnd
                        },
                        new MenuItem()
                        {
                            Id = 9,
                            DisplayNameLocal = "Google Search",
                            DisplayNameInternational = "Google Search",
                            MenuItemType = MenuItemTypes.CustomLink,
                            Link = "www.google.com?search=bforms",
                            Icon = Glyphicon.Globe,
                            Visibility = MenuItemVisibility.FrontEnd
                        },
                        new MenuItem()
                        {
                            Id = 10,
                            DisplayNameLocal = "Bing Search",
                            DisplayNameInternational = "Bing Search",
                            MenuItemType = MenuItemTypes.CustomLink,
                            Link = "www.bing.com?search=bforms",
                            Icon = Glyphicon.Globe,
                            Visibility = MenuItemVisibility.FrontEnd
                        }
                    };
            #endregion
        }

        public List<MenuItem> MenuItems { get; set; }

        public void SaveChanges()
        {
            HttpContext.Current.Session["DbContext"] = this;
        }

        public static BFormsContext Get()
        {
            var sessionContext = (BFormsContext)HttpContext.Current.Session["DbContext"];

            if (sessionContext == null)
            {
                sessionContext = new BFormsContext();

                HttpContext.Current.Session["DbContext"] = sessionContext;
            }

            return sessionContext;
        }
    }
    #endregion

    #region MenuItems
    public class MenuItem
    {
        public int Id { get; set; }
        public string DisplayNameLocal { get; set; }
        public string DisplayNameInternational { get; set; }
        public MenuItemTypes MenuItemType { get; set; }
        public string Link { get; set; }
        public Glyphicon? Icon { get; set; }
        public MenuItemVisibility Visibility { get; set; }
    }
    #endregion
}
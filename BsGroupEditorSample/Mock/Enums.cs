using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Menu.Resources;

namespace Menu.Mock
{
    public enum MenuTypes
    {
        [Display(Name = "PublicMenu", ResourceType = typeof(Resource))]
        PublicMenu = 1,
        [Display(Name = "UsersMenu", ResourceType = typeof(Resource))]
        UsersMenu = 2,
        [Display(Name = "AdminMenu", ResourceType = typeof(Resource))]
        AdminMenu = 3
    }

    public enum MenuItemTypes
    {
        [Display(Name = "Page", ResourceType = typeof(Resource))]
        Page = 1,
        [Display(Name = "CustomLink", ResourceType = typeof(Resource))]
        CustomLink = 2,
        [Display(Name = "Category", ResourceType = typeof(Resource))]
        Category = 3
    }

    public enum MenuItemVisibility
    {
        [Display(Name = "PublicArea", ResourceType = typeof(Resource))]
        PublicArea = 1,
        [Display(Name = "UsersArea", ResourceType = typeof(Resource))]
        UsersArea = 2,
        [Display(Name = "AdminArea", ResourceType = typeof(Resource))]
        AdminArea = 3,
        [Display(Name = "Any", ResourceType = typeof(Resource))]
        Any = 4
    }
}
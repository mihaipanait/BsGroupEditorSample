using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BsGroupEditorSample.Resources;

namespace BsGroupEditorSample.Mock
{
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
        [Display(Name = "Both", ResourceType = typeof(Resource))]
        Both = 1,
        [Display(Name = "FrontEnd", ResourceType = typeof(Resource))]
        FrontEnd = 2,
        [Display(Name = "BackEnd", ResourceType = typeof(Resource))]
        BackEnd = 3,
        [Display(Name = "Any", ResourceType = typeof(Resource))]
        Any = 4
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BForms.Models;
using BForms.Mvc;
using BsGroupEditorSample.Mock;
using BsGroupEditorSample.Resources;

namespace BsGroupEditorSample.Models
{
    public class SampleGroupRowModel : BsEditorGroupItemModel<SampleRowFormModel>
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }

        public override object GetUniqueID()
        {
            return this.Id;
        }
    }

    public class SampleRowFormModel
    {
        [Display(Name = "DisplayName", ResourceType = typeof(Resource))]
        [Required]
        [BsControl(BsControlType.TextBox)]
        public string Name { get; set; }

        [Display(Name = "Link", Prompt = "PromptLink", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Link { get; set; }
    }

    public class GroupEditorModel
    {
        [BsEditorTab(Name = "Pages", Id = MenuItemTypes.Page, Selected = false)]
        public BsEditorTabModel<SampleGroupRowModel, MenuItemSearchModel, PageNewModel> Tab1 { get; set; }

        [BsEditorTab(Name = "Custom Links", Id = MenuItemTypes.CustomLink, Selected = false)]
        public BsEditorTabModel<SampleGroupRowModel, MenuItemSearchModel, CustomLinkNewModel> Tab2 { get; set; }

        [BsEditorTab(Name = "Categories", Id = MenuItemTypes.Category, Selected = false)]
        public BsEditorTabModel<SampleGroupRowModel, MenuItemSearchModel, CategoryNewModel> Tab3 { get; set; }

        [BsEditorGroup(Id = MenuTypes.MainMenu)]
        public BsEditorGroupModel<SampleGroupRowModel> Group1 { get; set; }

        [BsEditorGroup(Id = MenuTypes.FooterMenu)]
        public BsEditorGroupModel<SampleGroupRowModel> Group2 { get; set; }

        [BsEditorGroup(Id = MenuTypes.AdminMenu)]
        public BsEditorGroupModel<SampleGroupRowModel> Group3 { get; set; }
    }

    public class MenuItemSearchModel
    {
        public MenuItemSearchModel()
        {
            Visibility = new BsSelectList<MenuItemVisibility?>();
            Visibility.ItemsFromEnum(typeof(MenuItemVisibility));
            Visibility.SelectedValues = MenuItemVisibility.Both;
        }

        [Display(Name = "DisplayName", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string DisplayName { get; set; }

        [BsControl(BsControlType.RadioButtonList)]
        [Display(Name = "Visibility", ResourceType = typeof(Resource))]
        public BsSelectList<MenuItemVisibility?> Visibility { get; set; }
    }

    public class MenuItemNewModel
    {
        public MenuItemNewModel()
        {
            Visibility = new BsSelectList<MenuItemVisibility>();
            Visibility.ItemsFromEnum(typeof(MenuItemVisibility), MenuItemVisibility.Any);
            Visibility.SelectedValues = MenuItemVisibility.Both;

            Icon = new BsSelectList<Glyphicon?>();
            Icon.ItemsFromEnum(typeof(Glyphicon));
        }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [BsControl(BsControlType.RadioButtonList)]
        [Display(Name = "Visibility", ResourceType = typeof(Resource))]
        public BsSelectList<MenuItemVisibility> Visibility { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "DisplayName", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string DisplayName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Link", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Link { get; set; }

        [Display(Name = "Icon", Prompt = "PromptIcon", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<Glyphicon?> Icon { get; set; }

        public virtual MenuItemTypes MenuItemType { get; set; }
    }

    public sealed class PageNewModel : MenuItemNewModel
    {
        public PageNewModel() : base()
        {
            MenuItemType = MenuItemTypes.Page;
        }

        public override MenuItemTypes MenuItemType { get; set; }
    }

    public sealed class CategoryNewModel : MenuItemNewModel
    {
        public CategoryNewModel() : base()
        {
            MenuItemType = MenuItemTypes.Category;
        }

        public override MenuItemTypes MenuItemType { get; set; }
    }

    public sealed class CustomLinkNewModel : MenuItemNewModel
    {
        public CustomLinkNewModel() : base()
        {
            MenuItemType = MenuItemTypes.CustomLink;
        }

        public override MenuItemTypes MenuItemType { get; set; }
    }
}
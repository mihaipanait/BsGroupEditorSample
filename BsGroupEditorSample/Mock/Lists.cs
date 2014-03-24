using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BForms.Models;
using System.Collections.Generic;

namespace Menu.Mock
{
    public static class Lists
    {
        public static BsSelectList<T> AllMenuItemTypes<T>()
        {
            var list = new BsSelectList<T>();
            list.ItemsFromEnum(typeof(MenuItemTypes));

            return list;
        }

        public static BsSelectList<T> AllVisibilityTypes<T>()
        {
            var list = new BsSelectList<T>();
            list.ItemsFromEnum(typeof(MenuItemVisibility));

            return list;
        }

        public static BsSelectList<T> VisibilityTypes<T>()
        {
            var list = new BsSelectList<T>();
            list.ItemsFromEnum(typeof(MenuItemVisibility), MenuItemVisibility.Any);

            return list;
        }
    }
}
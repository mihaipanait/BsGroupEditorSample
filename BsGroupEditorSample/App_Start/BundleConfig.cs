using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BsGroupEditorSample.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/BForms")
                //BForms CSS bundle
                .Include("~/Scripts/BForms/Bundles/css/*.css", new CssRewriteUrlTransform())
                );
        }
    }
}


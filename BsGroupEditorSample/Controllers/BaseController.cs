﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RequireJS;

namespace BsGroupEditorSample.Controllers
{
    public class BaseController : RequireJS.RequireJsController
    {
        public override void RegisterGlobalOptions()
        {
            RequireJsOptions.Add(
                "homeUrl",
                Url.Action("Index", "Home", new { area = "" }),
                RequireJsOptionsScope.Global);
        }
    }
}
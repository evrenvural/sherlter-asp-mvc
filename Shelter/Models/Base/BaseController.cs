using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Shelter.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Models.Base
{
    public abstract class BaseController : Controller
    {
        readonly ILogger<BaseController> _logger;
        readonly IStringLocalizer<BaseController> _localizer;

        public BaseController(ILogger<BaseController> logger, IStringLocalizer<BaseController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Not View Methods
        protected void FillViewDatas()
        {
            foreach (var item in _localizer.GetAllStrings())
            {
                ViewData[item.Name] = item.Value;
            }
        }
    }
}

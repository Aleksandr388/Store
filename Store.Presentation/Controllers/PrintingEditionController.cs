using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    public class PrintingEditionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

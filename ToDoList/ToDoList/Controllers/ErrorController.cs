using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult SomethingWrong() {
            return View();
        }
        public ActionResult UnknownError() {
            return View();
        }
    }
}
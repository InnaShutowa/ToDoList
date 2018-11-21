using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            using (var db = new ListEntities()) {
                return View(db.ToDo.ToList());
            }
        }

        public ActionResult ChangeStyle(int id) {
            try {
                using (var db = new ListEntities()) {
                    var element = db.ToDo.ToList();
                    element.FirstOrDefault(a => a.Id == id).Status = !element.FirstOrDefault(a => a.Id == id).Status;
                    db.SaveChanges();
                    return View("Index", element);
                }
            } catch (Exception) {
                return Redirect("/Error/UnknownError");
            }
        }

        public ActionResult Delete(int id) {
            using (var db = new ListEntities()) {
                var element = db.ToDo.FirstOrDefault(a => a.Id == id);
                if (element==null) return Redirect("/Error/NotFound");
                db.ToDo.Remove(element);
                db.SaveChanges();
                return View("Index", db.ToDo.ToList());
            }
        }
        [HttpGet]
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ToDo element) {
            using (var db = new ListEntities()) {
                if (element.DateCreate > element.DateDeadline ||
                   String.IsNullOrEmpty(element.Text) || String.IsNullOrEmpty(element.Title))
                    return Redirect("/Error/SomethingWrong");
                db.ToDo.Add(element);
                db.SaveChanges();
                return View("Index", db.ToDo.ToList());
            }
        }
        [HttpGet] 
        public ActionResult Edit(int id) {
            using (var db = new ListEntities()) {
                if (!db.ToDo.Any(a => a.Id == id)) return Redirect("/Error/NotFound");
                return View(db.ToDo.FirstOrDefault(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult Edit(ToDo element) {
            using (var db = new ListEntities()) {
                if (!db.ToDo.Any(a => a.Id == element.Id)) return Redirect("/Error/NotFound");
                if (//element.DateCreate > element.DateDeadline || 
                    String.IsNullOrEmpty(element.Text) || String.IsNullOrEmpty(element.Title))
                    return Redirect("/Error/SomethingWrong");
                var oldElement = db.ToDo.FirstOrDefault(a => a.Id == element.Id);
                oldElement.Title = element.Title;
                oldElement.Text = element.Text;
                oldElement.Status = element.Status;
                oldElement.DateDeadline = element.DateDeadline;
                oldElement.DateCreate = element.DateCreate;
                db.SaveChanges();
                return View("Index", db.ToDo.ToList());
            }
        }
    }
}
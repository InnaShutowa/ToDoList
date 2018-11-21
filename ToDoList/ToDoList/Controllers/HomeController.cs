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
        public ActionResult Index()
        {
            using (var db = new ListEntities()) {
                return View(db.ToDo.ToList());
            }
        }

        public ActionResult Delete(int id) {
            using (var db = new ListEntities()) {
                var element = db.ToDo.FirstOrDefault(a => a.Id == id);
                if (element != null) db.ToDo.Remove(element);
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
                db.ToDo.Add(element);
                db.SaveChanges();
                return View("Index", db.ToDo.ToList());
            }
        }
        [HttpGet]
        public ActionResult Edit(int id) {
            using (var db = new ListEntities()) {
                if (!db.ToDo.Any(a => a.Id == id)) return null;
                return View(db.ToDo.FirstOrDefault(a => a.Id == id));
            }
        }
        [HttpPost]
        public ActionResult Edit(ToDo element) {
            using (var db = new ListEntities()) {
                if (!db.ToDo.Any(a => a.Id == element.Id)) return null;
                if (element.DateCreate < element.DateDeadline) return null;
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
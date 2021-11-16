using DapperBasicCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperBasicCrud.Controllers
{
    public class Friends : Controller
    {
        private FriendsRepository repository=new FriendsRepository();
        // GET: Friends
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        // GET: Friends/Details/5
        public ActionResult Details(int id)
        {
            return View(repository.Get(id));
        }

        // GET: Friends/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        [HttpPost]
        public ActionResult Create(Friend friend)
        {
            if (ModelState.IsValid)
            {
                repository.Create(friend);
                return RedirectToAction("Index");
            }         
            return View();           
        }

        // GET: Friends/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Friends/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Friend friend)
        {
            if (ModelState.IsValid)
            {
                repository.Create(friend);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Friends/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Friends/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Friend friend)
        {
            repository.Delete(friend.Id);
            return RedirectToAction("Index");           
                       
        }
    }
}

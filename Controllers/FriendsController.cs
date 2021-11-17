using DapperBasicCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperBasicCrud.Controllers
{
    public class FriendsController : Controller
    {
        private FriendsRepository repository;
         
        public FriendsController()
        {
            repository = new FriendsRepository();
        }
        
        // GET: Friends
        public ActionResult Index(RequestModel request)
        {
            if (request.OrderBy == null)
            {
                request = new RequestModel
                {
                    Search = request.Search,
                    OrderBy = "name",
                    IsDescending = false
                };
            }
            ViewBag.Request = request;
            return View(repository.GetAll(request));
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
        public ActionResult Create(Friend friend, bool editAfterSaving = false)
        {
            if (ModelState.IsValid)
            {
                var lastInsertedId = repository.Create(friend);
                if (lastInsertedId > 0)
                {
                    TempData["Message"] = "Record added successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to add record";
                }
                if (editAfterSaving)
                {
                    return RedirectToAction("Edit", new { Id = lastInsertedId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Friends/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repository.Get(id));
        }

        // POST: Friends/Edit/5
        [HttpPost]
        public ActionResult Edit(Friend friend)
        {
            if (ModelState.IsValid)
            {
                var recordAffected = repository.Update(friend);
                if (recordAffected > 0)
                {
                    TempData["Message"] = "Record updated successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to update record";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Friends/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.Get(id));
        }

        // POST: Friends/Delete/5
        [HttpPost]
        public ActionResult Delete(Friend friend)
        {
            var recordAffected = repository.Delete(friend.Id);
            if (recordAffected > 0)
            {
                TempData["Message"] = "Record deleted successfully";
            }
            else
            {
                TempData["Error"] = "Failed to delete record";
            }
            return RedirectToAction("Index");

        }
    }
}

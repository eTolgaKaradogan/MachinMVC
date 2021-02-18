using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _02_DataEntities.Entities;
using _03_DataAccess.Context;
using _04_Business.Models;
using _04_Business.Services;
using AppCore.Business.Bases;
using AppCore.DataAccess.Repositories;
using AppCore.DataAccess.Repositories.Bases;

namespace _05_MI_MvcWebUl.Controllers
{
    public class FactoriesController : Controller
    {
        private readonly MI_Context db;
        private readonly RepositoryBase<Factory> factoryRepository;
        private readonly IService<Factory, FactoryModel> factoryService;
        private readonly RepositoryBase<User> userRepository;
        private readonly IService<User, UserModel> userService;
        public FactoriesController()
        {
            db = new MI_Context();
            factoryRepository = new Repository<Factory>(db);
            factoryService = new FactoryService(factoryRepository);
            userRepository = new Repository<User>(db);
            userService = new UserService(userRepository);
        }

        // GET: Factories
        public ActionResult Index()
        {
            var username = User.Identity.Name.ToString();
            var sr = userService.GetQuery(q => q.Name == username).FirstOrDefault();
            if (sr==null)
            {
                return View();
            }
            var liste = factoryService.GetQuery(q => q.Id == sr.FactoryId).ToList();
            return View(liste);
        }

        // GET: Factories/Details/5
        public ActionResult Details(int id)
        {
            FactoryModel model = factoryService.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Factories/Create
        public ActionResult Create()
        {
            var model = new FactoryModel();
            return View(model);
        }

        // POST: Factories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FactoryModel model)
        {
            if (ModelState.IsValid)
            {
                factoryService.Add(model);
                factoryService.SaveChanges();
                //var list = new SelectList(new List<SelectListItem>
                //{
                //     new SelectListItem { Selected = true, Text = model.Name.ToString(), Value =model.Id.ToString()},

                //});
                var model2 = factoryService.GetQuery(q => q.Name == model.Name).FirstOrDefault();
                TempData["FabrikaId"] = model2.Id;
                TempData.Keep(key: "FabrikaId");
                return RedirectToAction("YeniKayit", "Account");
            }

            return View(model);
        }

        // GET: Factories/Edit/5
        public ActionResult Edit(int id)
        {
            FactoryModel model = factoryService.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Factories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FactoryModel model)
        {
            if (ModelState.IsValid)
            {
                factoryService.Update(model);
                factoryService.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Factories/Delete/5
        public ActionResult Delete(int id)
        {
            FactoryModel model = factoryService.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Factories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            factoryService.Delete(id);
            factoryService.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

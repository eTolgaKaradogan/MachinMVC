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
    public class SectionsController : Controller
    {
        private readonly MI_Context db;
        private readonly RepositoryBase<Section> sectionRepository;
        private readonly IService<Section, SectionModel> sectionService;
        private readonly RepositoryBase<User> userRepository;
        private readonly IService<User, UserModel> userService;
        private readonly RepositoryBase<Factory> factoryRepository;
        private readonly IService<Factory, FactoryModel> factoryService;
        public SectionsController()
        {
            db = new MI_Context();
            sectionRepository = new Repository<Section>(db);
            sectionService = new SectionService(sectionRepository);
            factoryRepository = new Repository<Factory>(db);
            factoryService = new FactoryService(factoryRepository);
            userRepository = new Repository<User>(db);
            userService = new UserService(userRepository);
        }
        // GET: Sections
        public ActionResult Index()
        {
            var user = User.Identity.Name.ToString();
            var list = userService.GetQuery(q => q.Name == user).FirstOrDefault();
            var sections = sectionService.GetQuery(q => q.FactoryId == list.FactoryId);
            return View(sections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int id)
        {
            SectionModel section = sectionService.GetById(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Sections/Create
        public ActionResult Create()
        {
            var model = new SectionModel();
            //ViewBag.FactoryId = new SelectList(sectionService.GetQuery().ToList(), "Id", "Name");
            return View(model);
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectionModel model)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name.ToString();
                var userdb = userService.GetQuery(q => q.Name == user).FirstOrDefault();
                model.FactoryId = userdb.FactoryId;
                sectionService.Add(model);
                sectionService.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FactoryId = new SelectList(sectionService.GetQuery().ToList(), "Id", "Name", model.FactoryId);
            return View();
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int id)
        {

            SectionModel section = sectionService.GetById(id);
            if (section == null)
            {
                return HttpNotFound();
            }
           
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionModel section)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name.ToString();
                var userdb = userService.GetQuery(q => q.Name == user).FirstOrDefault();
                section.FactoryId = userdb.FactoryId;
                sectionService.Update(section);
                sectionService.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(section);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int id)
        {
            SectionModel section = sectionService.GetById(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sectionService.Delete(id);
            sectionService.SaveChanges();
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

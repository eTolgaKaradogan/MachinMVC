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
    public class MachinesController : Controller
    {
        private readonly MI_Context db;
        private readonly RepositoryBase<Machine> machineRepository;
        private readonly IService<Machine, MachineModel> machineService;
        private readonly RepositoryBase<Section> secrepositoy;
        private readonly IService<Section, SectionModel> secService;
        private readonly RepositoryBase<User> userRepository;
        private readonly IService<User, UserModel> userService;

        public MachinesController()
        {
            db = new MI_Context();
            machineRepository = new Repository<Machine>(db);
            machineService = new MachineService(machineRepository);
            secrepositoy = new Repository<Section>(db);
            secService = new SectionService(secrepositoy);
            userRepository = new Repository<User>(db);
            userService = new UserService(userRepository);
        }

        // GET: Machines
        public ActionResult Index()
        {
            var user = User.Identity.Name.ToString();
            var dbdengelenUser=userService.GetQuery(q => user==q.Name).FirstOrDefault();
            var sectionList=secService.GetQuery(q => q.FactoryId == dbdengelenUser.FactoryId).ToList();
            var machineList = machineService.GetQuery().ToList();
           
            var maclist=machineList.Where(q=>sectionList.Any(w=>w.Id==q.SectionId)).ToList();
                    
            return View(maclist);
        }

        // GET: Machines/Details/5
        public ActionResult Details(int id)
        {

            MachineModel machine = machineService.GetById(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // GET: Machines/Create
        public ActionResult Create()
        {
            var userstring = User.Identity.Name.ToString();
            var user=userService.GetQuery(q => q.Name == userstring).FirstOrDefault();
            var qw = secService.GetQuery(q=>q.FactoryId==user.FactoryId).ToList();
            ViewBag.List = new SelectList(qw, "Id", "Name");
            var model = new MachineModel();
            return View(model);
        }

        // POST: Machines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MachineModel machine)
        {
            if (ModelState.IsValid)
            {
                machineService.Add(machine);
                machineService.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machine);
        }

        // GET: Machines/Edit/5
        public ActionResult Edit(int id)
        {
            var userstring = User.Identity.Name.ToString();
            var user = userService.GetQuery(q => q.Name == userstring).FirstOrDefault();
            var qw = secService.GetQuery(q => q.FactoryId == user.FactoryId).ToList();
            ViewBag.List = new SelectList(qw, "Id", "Name");
            MachineModel machine = machineService.GetById(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MachineModel machine)
        {
            if (ModelState.IsValid)
            {
                machineService.Update(machine);
                machineService.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machine);
        }

        // GET: Machines/Delete/5
        public ActionResult Delete(int id)
        {

            MachineModel machine = machineService.GetById(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            machineService.Delete(id);
            machineService.SaveChanges();
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

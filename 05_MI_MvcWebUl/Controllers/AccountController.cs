using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _02_DataEntities.Entities;
using _03_DataAccess.Context;
using _04_Business.Models;
using _04_Business.Services;
using AppCore.Business.Bases;
using AppCore.DataAccess.Repositories;
using AppCore.DataAccess.Repositories.Bases;

namespace _05_MI_MvcWebUl.Controllers
{
    public class AccountController : Controller
    {
        private readonly MI_Context db;
        private readonly RepositoryBase<User> userRepository;
        private readonly RepositoryBase<Role> roleRepository;
        private readonly RepositoryBase<Factory> factoryRepository;
        private readonly IService<User, UserModel> userService;
        private readonly IService<Role, RoleModel> roleService;
        private readonly IService<Factory, FactoryModel> factoryService;
        public AccountController()
        {
            db = new MI_Context();
            userRepository = new Repository<User>(db);
            userService = new UserService(userRepository);
            roleRepository = new Repository<Role>(db);
            roleService = new RoleService(roleRepository);
            factoryRepository = new Repository<Factory>(db);
            factoryService = new FactoryService(factoryRepository);
        }

        // GET: Account
        public ActionResult Index()
        {
            var users = userService.GetQuery();
            return View(users.ToList());
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {

            UserModel user = userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Account/Create
        //public ActionResult Create()
        //{
        //    var model = new UserModel();
        //    ViewBag.RoleId = new SelectList(roleService.GetQuery().ToList(), "Id", "Name");
        //    return View();
        //}

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(UserModel user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        userService.Add(user);
        //        userService.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.RoleId = new SelectList(roleService.GetQuery().ToList(), "Id", "Name", user.RoleId);
        //    return View(user);
        //}

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {

            UserModel user = userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(roleService.GetQuery().ToList(), "Id", "Name", user.RoleId);
            return View(user);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                userService.Update(user);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(roleService.GetQuery().ToList(), "Id", "Name", user.RoleId);
            return View(user);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            UserModel user = userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userService.Delete(id);
            userService.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {
            try
            {
                bool userFound = userService.GetQuery().Any(u => u.Name.ToUpper() == user.Name.ToUpper() && u.Password == user.Password);

                if (!userFound)
                {
                    ViewBag.Message = "Girdiğiniz Bilgiler Yanlıştır.Kontrol ediniz!";
                    return View(user);
                }
                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "An error occured!");
            }
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult YeniKayit()
        {
            var model = new UserModel();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniKayit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                bool passwordcontrol = string.Equals(model.Password, model.PasswordAgain);
                if (!passwordcontrol)
                {
                    ViewBag.Message = "Hatalı Şifre";
                }
                bool mailcontrol = string.Equals(model.Mail, model.MailAgain);
                if (!mailcontrol)
                {
                    ViewBag.Message = "Hatalı Mail Adresi";
                }
                model.RoleId = (int)AppCore.Business.Enums.RoleEnum.Admin;
                var uy = TempData["FabrikaId"].ToString();
                model.FactoryId = Convert.ToInt32(uy);
                userService.Add(model);
                userService.SaveChanges();
                ViewBag.LoginName = model.Name;
                ViewBag.Message = "İşlem Başarı ile tamamlandı.Bir sonraki kayıt işlemine geçiliyor. ";
                return RedirectToAction("YeniKayitSonrası");
            }
            return View(model);
        }

        public ActionResult YeniKayitSonrası()
        {
            return View();
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

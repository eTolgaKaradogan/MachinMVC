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
using System.Net.Mail;

namespace _05_MI_MvcWebUl.Controllers
{
    public class HatırlatıcıController : Controller
    {
        private readonly MI_Context db;
        private readonly RepositoryBase<Hatırlatıcı> hatRepositoryBase;
        private readonly IService<Hatırlatıcı, HatırlatıcıModel> hatService;
        private readonly RepositoryBase<User> userRepository;
        private readonly IService<User, UserModel> userService;

        public HatırlatıcıController()
        {
            db = new MI_Context();
            hatRepositoryBase = new Repository<Hatırlatıcı>(db);
            hatService = new HatirlaticiService(hatRepositoryBase);
            userRepository = new Repository<User>(db);
            userService = new UserService(userRepository);
        }

        // GET: Hatırlatıcı
        public ActionResult Sergi(int id)
        {
            var list = hatService.GetQuery(q => q.MachineId == id).ToList();
            return View(list);
        }

        // GET: Hatırlatıcı/Details/5
        public ActionResult Details(int id)
        {
            HatırlatıcıModel hatırlatıcı = hatService.GetById(id);
            if (hatırlatıcı == null)
            {
                return HttpNotFound();
            }
            return View(hatırlatıcı);
        }

        // GET: Hatırlatıcı/Create
        public ActionResult Create(int id)
        {
            HatırlatıcıModel model = new HatırlatıcıModel()
            {
                MachineId = id,
            };
            return View(model);
        }

        // POST: Hatırlatıcı/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HatırlatıcıModel hatırlatıcı)
        {
            //var userstring = User.Identity.Name.ToString();
            //var user = userService.GetQuery(q => q.Name == userstring).FirstOrDefault();
            //user.Mail = hatırlatıcı.From;
            if (DateTime.Now.ToShortDateString() != hatırlatıcı.DateTime.ToShortDateString())
            {
                ViewBag.Message = "Hatırlatıcı başarı ile oluşturulmuştur.";
            }
            else
            {
                MailMessage mail = new MailMessage(hatırlatıcı.From, hatırlatıcı.To);
                mail.Subject = hatırlatıcı.Name;
                mail.Body = hatırlatıcı.Details;
                mail.IsBodyHtml = false;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential network = new NetworkCredential(hatırlatıcı.From, hatırlatıcı.GmailPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = network;
                smtp.Send(mail);
            }
            if (ModelState.IsValid)
            {
                hatService.Add(hatırlatıcı);
                hatService.SaveChanges();
                return RedirectToAction("Sergi", new { id = hatırlatıcı.MachineId });
            }

            return View(hatırlatıcı);
        }

        // GET: Hatırlatıcı/Edit/5
        public ActionResult Edit(int id)
        {
            HatırlatıcıModel hatırlatıcı = hatService.GetById(id);
            if (hatırlatıcı == null)
            {
                return HttpNotFound();
            }
            return View(hatırlatıcı);
        }

        // POST: Hatırlatıcı/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HatırlatıcıModel hatırlatıcı)
        {
            if (ModelState.IsValid)
            {
                var entity = hatService.GetById(hatırlatıcı.Id);
                entity.Name = hatırlatıcı.Name;
                entity.Details = hatırlatıcı.Details;
                entity.DateTime = hatırlatıcı.DateTime;
                entity.MachineId = hatırlatıcı.MachineId;
                hatService.Update(entity);
                hatService.SaveChanges();
                return RedirectToAction("Sergi", new { id = hatırlatıcı.MachineId });
            }
            return View(hatırlatıcı);
        }

        // GET: Hatırlatıcı/Delete/5
        public ActionResult Delete(int id)
        {

            HatırlatıcıModel model = hatService.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Hatırlatıcı/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = hatService.GetById(id);
            hatService.Delete(id);
            hatService.SaveChanges();
            return RedirectToAction("Sergi", new { id = model.MachineId });
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

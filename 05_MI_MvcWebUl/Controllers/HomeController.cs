using _02_DataEntities.Entities;
using _03_DataAccess.Context;
using _04_Business.Models;
using _04_Business.Services;
using AppCore.Business.Bases;
using AppCore.DataAccess.Repositories;
using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _05_MI_MvcWebUl.Controllers
{
    public class HomeController : Controller
    {
        private readonly MI_Context db;
        private readonly RepositoryBase<Factory> factoryRepository;
        private readonly IService<Factory, FactoryModel> factoryService;
        private readonly RepositoryBase<User> userRepository;
        private readonly IService<User, UserModel> userService;
        private readonly RepositoryBase<Machine> machineRepository;
        private readonly IService<Machine, MachineModel> machineService;
        private readonly RepositoryBase<Section> secrepositoy;
        private readonly IService<Section, SectionModel> secService;
        private readonly RepositoryBase<Hatırlatıcı> hatRepositoryBase;
        private readonly IService<Hatırlatıcı, HatırlatıcıModel> hatService;

        public HomeController()
        {
            db = new MI_Context();
            factoryRepository = new Repository<Factory>(db);
            factoryService = new FactoryService(factoryRepository);
            userRepository = new Repository<User>(db);
            userService = new UserService(userRepository);
            machineRepository = new Repository<Machine>(db);
            machineService = new MachineService(machineRepository);
            secrepositoy = new Repository<Section>(db);
            secService = new SectionService(secrepositoy);
            hatRepositoryBase = new Repository<Hatırlatıcı>(db);
            hatService = new HatirlaticiService(hatRepositoryBase);
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userstring = User.Identity.Name.ToString();
                var user = userService.GetQuery(q => q.Name == userstring).FirstOrDefault();
                var factory = factoryService.GetQuery(q => q.Id == user.FactoryId).FirstOrDefault();
                if (factory == null)
                {
                    return View();

                }
                var sections = secService.GetQuery(a => a.FactoryId == factory.Id).ToList();
                if (sections == null)
                {
                    return View();
                }
                var machinesList = machineService.GetQuery().ToList();
                var machines = machinesList.Where(a => sections.Any(e => e.Id == a.SectionId)).ToList();
                if (machines == null)
                {
                    return View();
                }
                var hatList = hatService.GetQuery().ToList();
                var hats = hatList.Where(r => machines.Any(y => y.Id == r.MachineId)).ToList();
                return View(hats);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Hak()
        {
            return View();
        }
        public ActionResult Ref()
        {
            return View();
        }
        public ActionResult Ile()
        {
            return View();
        }
    }
}
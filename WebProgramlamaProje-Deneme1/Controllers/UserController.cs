using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje_Deneme1.Models;

namespace WebProgramlamaProje_Deneme1.Controllers
{
    public class UserController : Controller
    {
        INewService newService = new NewsManager(new EfNewsDal());
        IBranchService branchService = new BranchManager(new EfBranchDal());
        IUserService userService = new UserManager(new EfUserDal(), new EfBasketDal());
        IAdminService adminService = new AdminManager(new EfAdminDal());
        ICarService carService = new CarManager(new EfCarDal());
        IDealService dealService = new DealManager(new EfRentDealDal());
        IMessageService messageService = new MessageManager(new EfMessageDal());
        // GET: UserController
        public ActionResult Index()
        {

            indexData d = new indexData
            {
                News = newService.GetAll().Data,
                Branches = branchService.GetAll().Data
            };

            return View(d);
        }

        public ActionResult Giris(string Email, string Password)
        {
            if (userService.GetByMail(Email).Success)
            {
                User user = userService.GetByMail(Email).Data;
                if (user.Password == Password)
                {
                    HttpContext.Session.SetInt32("IsAdmin", 0);
                    HttpContext.Session.SetString("Name", user.Name);
                    HttpContext.Session.SetString("Mail", Email);
                    return RedirectToAction("Index", "Customer");
                }
            }
            
            if(adminService.GetByMail(Email).Success)
            {
                if (adminService.GetByMail(Email).Data.Password == Password)
                {
                    HttpContext.Session.SetInt32("IsAdmin", 1);
                    HttpContext.Session.SetString("Mail", Email);
                    return RedirectToAction("AdminKayitliKullanicilar", "Admin");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KayitOlRes(string Name, string Email, string PhoneNumber, string Password, string P2)
        {
            userService.Add(new User {Name = Name, Email = Email, Password = Password, PhoneNumber = PhoneNumber, Basket = new Basket() });
            HttpContext.Session.SetInt32("IsAdmin", 0);
            HttpContext.Session.SetString("Name", Name);
            HttpContext.Session.SetString("Mail", Email);
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Haber(int id)
        {
            if (newService.GetById(id).Success)
            {
                return View(newService.GetById(id).Data);
            }
            return RedirectToAction("Index", "User");
        }
        public ActionResult CarList()
        {
            return View(new CarListModel { Cars =carService.GetAll().Data, Branches = branchService.GetAll().Data });
        }

        [HttpPost]
        public ActionResult CarListWF(DateTime rentDate, DateTime deliverDate , string brand = "", string fuelType = "", string typeOfGear = "", uint dailyPrice = 0, int branchId = -1)
        {
            if (brand == null)// bug fix : brand vs bugundan dolayı "" olsa bile null veriyor
            {
                brand = "";
            }
            if (rentDate > deliverDate)
            {
                deliverDate = rentDate;
            }
            return View(new CarListModel { Cars = new FilterModel { Brand = brand, BranchId = branchId, DailyPrice = dailyPrice, FuelType = fuelType, RentDate = rentDate, TypeOfGear = typeOfGear , DeliverDate = deliverDate}.Filt(carService, dealService), Branches = branchService.GetAll().Data } );
        }

        public ActionResult ContUs()
        {
            return View();
        }

        public ActionResult ContUpdate(string name, string mail , string content)
        {
            messageService.Add(new Message {OneWhoSendName = name, Email = mail, Content = content });
            return RedirectToAction("Index", "User");
        }

        public ActionResult EnterAgain()
        {
            return View();
        }
    }
}

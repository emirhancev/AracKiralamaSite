using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
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
    public class CustomerController : Controller
    {
        INewService newService = new NewsManager(new EfNewsDal());
        IBranchService branchService = new BranchManager(new EfBranchDal());
        ICarService carService = new CarManager(new EfCarDal());
        IDealService dealService = new DealManager(new EfRentDealDal());
        IMessageService messageService = new MessageManager(new EfMessageDal());
        IUserService userService = new UserManager(new EfUserDal(), new EfBasketDal());
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
            return RedirectToAction("Index","Customer");
        }
        public ActionResult CarList()
        {
            return View(new CarListModel { Cars = carService.GetAll().Data, Branches = branchService.GetAll().Data });
        }

        [HttpPost]
        public ActionResult CarListWF(DateTime rentDate, DateTime deliverDate, string brand = "", string fuelType = "", string typeOfGear = "", uint dailyPrice = 0, int branchId = -1)
        {
            if (brand == null)// bug fix : brand vs bugundan dolayı "" olsa bile null veriyor
            {
                brand = "";
            }
            if (rentDate > deliverDate)
            {
                deliverDate = rentDate;
            }
            return View(new CarListModel { Cars = new FilterModel { Brand = brand, BranchId = branchId, DailyPrice = dailyPrice, FuelType = fuelType, RentDate = rentDate, TypeOfGear = typeOfGear, DeliverDate = deliverDate }.Filt(carService, dealService), Branches = branchService.GetAll().Data });
        }

        public ActionResult ContUs()
        {
            return View();
        }

        public ActionResult ContUpdate( string content)
        {
            if (HttpContext.Session.GetInt32("IsAdmin").HasValue == false)
            {
                return RedirectToAction("EnterAgain", "User");
            }
            messageService.Add(new Message { OneWhoSendName = HttpContext.Session.GetString("Name"), Email = HttpContext.Session.GetString("Mail"), Content = content });
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult RentCar(int id)// admin ayarlandıktan sonra yapılacak
        {
            RentCarModel rentCarModel = new RentCarModel {Car = carService.GetById(id).Data };
            rentCarModel.Branch = branchService.GetById(rentCarModel.Car.BranchId).Data;
            return View(rentCarModel);
        }

        public ActionResult RentIt(DateTime rentDate, DateTime deliverDate, int carId)
        {
            if (HttpContext.Session.GetInt32("IsAdmin").HasValue == false)
            {
                return RedirectToAction("EnterAgain", "User");
            }
            if (dealService.Add(new RentDeal { Car = carService.GetById(carId).Data, RentDate = rentDate, DeliveryDate = deliverDate, User = userService.GetByMail(HttpContext.Session.GetString("Mail")).Data }).Success)
            {
                return RedirectToAction("ResComp", "Customer");
            }
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult ResComp()
        {
            return View();
        }

        public ActionResult KiralananAraclar()
        {
            if (HttpContext.Session.GetInt32("IsAdmin").HasValue == false)
            {
                return RedirectToAction("EnterAgain","User");
            }

            List<CustomerDealModel> models = new List<CustomerDealModel>();
            foreach (var item in dealService.GetDealsOfUser(userService.GetByMail(HttpContext.Session.GetString("Mail")).Data.Id).Data)
            {
                models.Add(new CustomerDealModel {  Car = carService.GetById(item.CarId).Data, Deal = item});
            }

            return View(models);
        }
    }
}

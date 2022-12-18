using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje_Deneme1.Models;

namespace WebProgramlamaProje_Deneme1.Controllers
{
    public class AdminController : Controller
    {
        IUserService userService = new UserManager(new EfUserDal(), new EfBasketDal());
        IMessageService messageService = new MessageManager(new EfMessageDal());
        ICarService carService = new CarManager(new EfCarDal());
        IBranchService branchService = new BranchManager(new EfBranchDal());
        INewService newService = new NewsManager(new EfNewsDal());
        IDealService dealService = new DealManager(new EfRentDealDal());
        public IActionResult AdminKayitliKullanicilar()
        {
            return View(userService.GetAll().Data);
        }
        // silme
        public IActionResult AdminMesajlar()
        {
            return View(messageService.GetAll().Data);
        }
        public IActionResult AdminMesajSil(int id)
        {
            if (messageService.GetById(id).Success == true)
            {
                messageService.Delete(messageService.GetById(id).Data);
            }
            return RedirectToAction("AdminMesajlar", "Admin");
        }
        public IActionResult AdminAraba()
        {
            return View(new CarListModel {Cars = carService.GetAll().Data, Branches = branchService.GetAll().Data });
        }

        public IActionResult ArabaSil(int id)
        {
            if (carService.GetById(id).Success)
            {
                carService.Delete(carService.GetById(id).Data);
            }
            return RedirectToAction("AdminAraba", "Admin");
        }

        public IActionResult ArabaEkle(string photoName, string brand, string fuelType, string typeOfGear, uint dailyPrice, int branchId, string description = "")
        {
            if (description == null) // bugfix
            {
                description = "";
            }
            carService.Add(new Car { Brand = brand, BranchId = branchId , DailyPrice = dailyPrice, Description = description, FuelType = fuelType, PhotoName = photoName, TypeOfGear = typeOfGear });
            return RedirectToAction("AdminAraba", "Admin");
        }

        public IActionResult AdminHaber()
        {
            return View(newService.GetAll().Data);
        }

        public IActionResult HaberEkle(string photoName, string title, string summary, string contents)
        {
            newService.Add(new News {  Contents = contents,  PhotoName = photoName, Summary = summary, Title = title});
            return RedirectToAction("AdminHaber", "Admin");
        }

        public IActionResult HaberSil(int id)
        {
            newService.Delete(newService.GetById(id).Data);
            return RedirectToAction("AdminHaber", "Admin");
        }

        public IActionResult AdminKiralananArabalar()
        {
            List<AdminDealModel> modelList = new List<AdminDealModel>();
            foreach (var item in dealService.GetAll().Data)
            {
                modelList.Add(new AdminDealModel { Car = carService.GetById(item.CarId).Data, Deal = item, User = userService.GetById(item.UserId).Data });
            }
            return View(modelList);
        }
        public IActionResult DeleteDeal(int id)
        {
            dealService.Delete(dealService.GetById(id).Data);
            return RedirectToAction("AdminKiralananArabalar", "Admin");
        }
    }
}

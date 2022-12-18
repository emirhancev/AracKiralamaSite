using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace WebProgramlamaProje_Deneme1.Models
{
    public class FilterModel
    {
        public string Brand = "";
        public string FuelType = "";
        public string TypeOfGear = "";
        public uint DailyPrice =0;
        public int BranchId = -1;
        public DateTime RentDate = DateTime.Today;
        public DateTime DeliverDate = DateTime.Today;

        public List<Car> Filt(ICarService carService,IDealService dealService)
        {
           

            if (BranchId == -1)
            {
                return FiltWOBranch(carService, dealService);
            }

            List<Car> carList= carService.GetAllWithLinq(x => x.Brand.Contains(Brand) &&
                                           x.DailyPrice >= DailyPrice &&
                                           x.FuelType.Contains(FuelType) &&
                                           x.TypeOfGear.Contains(TypeOfGear) &&
                                           x.BranchId == BranchId
            ).Data;
            for (int i = 0; i < carList.Count; i++)
            {
                foreach (var item in dealService.GetDealsOfCar(carList[i]).Data)
                {
                    if (!(item.DeliveryDate < RentDate || (item.RentDate > RentDate && item.RentDate > DeliverDate)))
                    {
                        carList.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }
            return carList;
        }
        public List<Car> FiltWOBranch(ICarService carService, IDealService dealService)
        {
            List<Car> carList = carService.GetAllWithLinq(x => x.Brand.Contains(Brand) &&
                                           x.DailyPrice >= DailyPrice &&
                                           x.FuelType.Contains(FuelType) &&
                                           x.TypeOfGear.Contains(TypeOfGear)
            ).Data;

            for (int i = 0; i<carList.Count;i++)
            {
                foreach (var item in dealService.GetDealsOfCar(carList[i]).Data)
                {
                    if (!(item.DeliveryDate < RentDate || (item.RentDate > RentDate && item.RentDate > DeliverDate)))
                    {
                        carList.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            return carList;
        }
    }
}

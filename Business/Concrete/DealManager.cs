using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class DealManager : IDealService
    {
        IRentDealDal _dealDal;
        public DealManager(IRentDealDal dealDal)
        {
            _dealDal = dealDal;
        }
        public IResult Add(RentDeal entity)
        {
            foreach (var item in GetDealsOfCar(entity.Car).Data)
            {
                if (!(item.DeliveryDate < entity.RentDate || (item.RentDate > entity.RentDate && item.RentDate > entity.DeliveryDate)))
                {
                    return new ErrorResult();
                }
            }
            _dealDal.Add(entity);
            return new SuccessResult(Messages.SuccessfullyAdded);
        }

        public IResult Delete(RentDeal entity)
        {
            _dealDal.Delete(entity);
            return new SuccessResult(Messages.SuccessfullyDeleted);
        }
        public IDataResult<List<RentDeal>> GetAll()
        {
            return new SuccessDataResult<List<RentDeal>>(_dealDal.GetAll().OrderBy(x=> x.UserId).ToList(), Messages.ObjectSuccessfullyReturned);
        }

        public IDataResult<RentDeal> GetById(int id)
        {
            return new SuccessDataResult<RentDeal>(_dealDal.Get(x => x.Id == id));
        }

        public IDataResult<List<RentDeal>> GetDealsOfCar(Car entity)
        {
            if (entity == null)
            {
                return new ErrorDataResult<List<RentDeal>>(null, Messages.ObjectNotFound);
            }
            return new SuccessDataResult<List<RentDeal>>(_dealDal.GetAll(x => x.Car.Id == entity.Id), Messages.ObjectSuccessfullyReturned);
        }

        public IDataResult<List<RentDeal>> GetDealsOfUser(int userId)
        {
            return new SuccessDataResult<List<RentDeal>>(_dealDal.GetAll(x => x.UserId == userId), Messages.ObjectSuccessfullyReturned);
        }

        public IResult Update(RentDeal entity)
        {
            return new SuccessResult(Messages.SuccessfullyUpdated);
        }
    }
}

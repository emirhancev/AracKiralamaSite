using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IDealService : IServiceRepository<RentDeal>
    {
        public IDataResult<List<RentDeal>> GetDealsOfCar(Car entity);
        public IDataResult<List<RentDeal>> GetDealsOfUser(int userId);
    }
}

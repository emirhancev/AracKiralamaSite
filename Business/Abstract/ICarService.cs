using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService:IServiceRepository<Car>
    {
        public IDataResult<List<Car>> GetAllByPrice(uint max, uint min);
        public IDataResult<List<Car>> GetAllWithLinq(Expression<Func<Car, bool>> filter = null);
    }
}

using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBranchService : IServiceRepository<Branch>
    {
        public IDataResult<Branch> GetByAdress(string Adress);
    }
}

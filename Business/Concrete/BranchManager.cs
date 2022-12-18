using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BranchManager : IBranchService
    {
        IBranchDal _branchDal;
        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
        }
        public IResult Add(Branch entity)
        {
            _branchDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Branch entity)
        {
            _branchDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Branch>> GetAll()
        {
            return new SuccessDataResult<List<Branch>>(_branchDal.GetAll());
        }

        public IDataResult<Branch> GetByAdress(string Adress)
        {
            return new SuccessDataResult<Branch>(_branchDal.Get(x => x.Adress.Contains(Adress)));
        }

        public IDataResult<Branch> GetById(int id)
        {
            return new SuccessDataResult<Branch>(_branchDal.Get(x => x.Id == id));
        }

        public IResult Update(Branch entity)
        {
            _branchDal.Update(entity);
            return new SuccessResult();
        }
    }
}

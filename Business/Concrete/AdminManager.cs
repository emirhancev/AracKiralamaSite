using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AdminManager: IAdminService
    {
        IAdminDal _adminDal;
        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public IResult Add(Admin entity)
        {
            if (GetByMail(entity.Email).Success )
            {
                return new ErrorResult(Messages.MailAlreadyInUser);
            }
            _adminDal.Add(entity);
            return new SuccessResult(Messages.SuccessfullyAdded);
        }

        public IResult Delete(Admin entity)
        {
            _adminDal.Delete(entity);
            return new SuccessResult(Messages.SuccessfullyDeleted);
        }

        public IDataResult<List<Admin>> GetAll()
        {
            return new SuccessDataResult<List<Admin>>(_adminDal.GetAll(), Messages.ObjectSuccessfullyReturned);
        }

        public IDataResult<Admin> GetById(int id)
        {
            Admin Admin = _adminDal.Get(x => x.Id == id);
            if (Admin == null)
            {
                return new ErrorDataResult<Admin>(Admin, Messages.ObjectNotFound);
            }
            return new SuccessDataResult<Admin>(Admin, Messages.ObjectSuccessfullyReturned);
        }

        public IDataResult<Admin> GetByMail(string mail)
        {
            Admin Admin = _adminDal.Get(x => x.Email == mail);
            if (Admin == null)
            {
                return new ErrorDataResult<Admin>(Admin, Messages.ObjectNotFound);
            }
            return new SuccessDataResult<Admin>(Admin, Messages.ObjectSuccessfullyReturned);
        }


        public IResult Update(Admin entity)
        {
            _adminDal.Update(entity);
            return new SuccessResult(Messages.SuccessfullyUpdated);
        }
    }
}

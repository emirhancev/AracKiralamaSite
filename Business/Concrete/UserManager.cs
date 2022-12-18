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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IBasketDal _basketDal;
        public UserManager(IUserDal userDal, IBasketDal basketDal)
        {
            _userDal = userDal;
            _basketDal = basketDal;
        }

        public IResult Add(User entity)
        {
            if (GetByMail(entity.Email).Success)
            {
                return new ErrorResult(Messages.MailAlreadyInUser);
            }
            _basketDal.Add(entity.Basket);
            _userDal.Add(entity);
            return new SuccessResult(Messages.SuccessfullyAdded);
        }

        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult(Messages.SuccessfullyDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.ObjectSuccessfullyReturned);
        }

        public IDataResult<User> GetById(int id)
        {
            User user = _userDal.Get(x => x.Id == id);
            if (user == null)
            {
                return new ErrorDataResult<User>(user, Messages.ObjectNotFound);
            }
            return new SuccessDataResult<User>(user, Messages.ObjectSuccessfullyReturned);
        }

        public IDataResult<User> GetByMail(string mail)
        {
            User user = _userDal.Get(x => x.Email == mail);
            if (user == null)
            {
                return new ErrorDataResult<User>(user, Messages.ObjectNotFound);
            }
            return new SuccessDataResult<User>(user, Messages.ObjectSuccessfullyReturned);
        }

        public IDataResult<User> GetByPhoneNumber(string phoneNum)
        {
            User user = _userDal.Get(x => x.PhoneNumber == phoneNum);
            if (user == null)
            {
                return new ErrorDataResult<User>(user, Messages.ObjectNotFound);
            }
            return new SuccessDataResult<User>(user, Messages.ObjectSuccessfullyReturned);
        }

        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult(Messages.SuccessfullyUpdated);
        }
    }
}

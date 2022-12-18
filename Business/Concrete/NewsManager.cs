using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class NewsManager : INewService
    {
        INewsDal _newsDal;
        public NewsManager(INewsDal newsDal)
        {
            _newsDal = newsDal;
        }
        public IResult Add(News entity)
        {
            _newsDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(News entity)
        {
            _newsDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<News>> GetAll()
        {
            return new SuccessDataResult<List<News>>(_newsDal.GetAll());
        }

        public IDataResult<News> GetById(int id)
        {
            News news = _newsDal.Get(x => x.Id == id);
            if (news == null)
            {
                return new ErrorDataResult<News>(news);
            }
            return new SuccessDataResult<News>(news);
        }

        public IResult Update(News entity)
        {
            _newsDal.Update(entity);
            return new SuccessResult();
        }
    }
}

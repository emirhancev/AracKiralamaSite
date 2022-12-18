using Core.Entities;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje_Deneme1.Models
{
    public class HTData<T> where T : class, IEntity, new()
    {
        public HTData()
        {
            User = null;
            Data = null;
        }
        public IUser User { get; set; }
        public T Data { get; set; }
    }
}

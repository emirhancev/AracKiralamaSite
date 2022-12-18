using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje_Deneme1.Models
{
    public class AdminDealModel
    {
        public RentDeal Deal { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
    }
}

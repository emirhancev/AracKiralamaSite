using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje_Deneme1.Models
{
    public class CarListModel
    {
        public List<Car> Cars { get; set; }
        public List<Branch> Branches { get; set; }
    }
}

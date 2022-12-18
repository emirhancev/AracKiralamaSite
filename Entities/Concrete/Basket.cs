using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Basket:IEntity
    {
        [Key]
        public int Id { get; set; }
        public List<RentDeal> Rents { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class RentDeal:IEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        [Required]
        public Car Car { get; set; }
        [Required]
        public DateTime RentDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public uint DailyPrice { get; set; }
        [MaxLength(10)]
        [Required]
        public string FuelType { get; set; }
        [MaxLength(15)]
        public string TypeOfGear { get; set; }
        [Required]
        public string PhotoName { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        [Required]
        public Branch Branch { get; set; }
    }
}

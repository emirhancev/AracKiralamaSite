using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Entities.Concrete
{
    public class Branch : IEntity 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Adress { get; set; }
    }
}

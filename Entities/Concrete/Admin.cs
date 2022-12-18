using Core.Entities;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Admin : IEntity, IUser 
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MaxLength(20)]
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
    }
}

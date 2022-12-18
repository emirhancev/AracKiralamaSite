using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class News: IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string PhotoName { get; set; }
        [Required]
        [MaxLength(250)]
        public string Summary { get; set; }
        [Required]
        public string Contents { get; set; }
    }
}

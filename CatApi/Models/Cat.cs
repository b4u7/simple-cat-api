using System;
using System.ComponentModel.DataAnnotations;

namespace CatApi.Models
{
    public class Cat
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public char Gender { get; set; }
        
        [Required]
        public DateTime Birthday { get; set; }
    }
}
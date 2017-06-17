using System.ComponentModel.DataAnnotations;

namespace WhatShouldIDoNow.Models
{
    public class CreateTaskViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public int ButtonInterval { get; set; }
        public int IntervalByHour { get; set; }
              
    }
}

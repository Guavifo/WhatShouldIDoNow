using System;

namespace WhatShouldIDoNow.DataAccess.Models
{
    public class AddCompletedTask
    {
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }
        public int UserId { get; set; }
    }
}

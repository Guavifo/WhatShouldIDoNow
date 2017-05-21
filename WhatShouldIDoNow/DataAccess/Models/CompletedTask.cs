using System;

namespace WhatShouldIDoNow.DataAccess.Models
{
    public class CompletedTask
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}

using System;

namespace WhatShouldIDoNow.DataAccess.Models
{
    public class AllActiveTask
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateStart { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }
    }
}

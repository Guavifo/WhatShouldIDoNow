using System;

namespace WhatShouldIDoNow.DataAccess.Models
{
    public class TaskToDo
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime LastViewed { get; set; }
        public DateTime DateStart { get; set; }
        public int TimesViewed { get; set; }
        public int IntervalByHour { get; set; }
    }
}

namespace WhatShouldIDoNow.DataAccess.Models
{
    public class CreateTask
    {
        public string Description { get; set; }
        public int IntervalByHour { get; set; }
        public int UserId { get; set; }
    }
}

﻿namespace Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public Address Address { get; set; } = new Address();
    }
}

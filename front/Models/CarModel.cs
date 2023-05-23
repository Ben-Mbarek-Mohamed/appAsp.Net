﻿using System.ComponentModel.DataAnnotations;

namespace front.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Type Type { get; set; }
    }
    public enum Status
    {
        New, Used
    }
    public enum Type
    {
        Suv, Sprot, Electrique, Sedan
    }
}


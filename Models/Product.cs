﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; }

        public string ShadeColour { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }
        public int CId { get; set; }

        





    }
}
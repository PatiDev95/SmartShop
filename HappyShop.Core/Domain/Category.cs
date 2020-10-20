﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyShop.Core.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Domain.Entities;

namespace WebApplication1.Dal.Context
{
    public class Application1DB : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public Application1DB(DbContextOptions<Application1DB> options) : base(options) { }
    }
}

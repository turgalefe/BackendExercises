﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TodoList
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
    }
}
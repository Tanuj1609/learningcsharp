﻿using JWTImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTImplementation.Context
{
    public class JwtContext : DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
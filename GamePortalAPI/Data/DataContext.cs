using System;
using GamePortalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePortalAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) {}

		//public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Teacher> Teachers => Set<Teacher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasKey("QuestionId");
        }
    }
}


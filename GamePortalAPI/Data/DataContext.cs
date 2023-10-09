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
        public DbSet<Question> Questions => Set<Question>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasKey("QuestionId");

            //This will change the table name to Question, this will be used when mapping.
            //Pluralizing is the convention in Entity Framework.
            var config = modelBuilder.Entity<Question>();
            config.ToTable("Question");
        }
    }
}


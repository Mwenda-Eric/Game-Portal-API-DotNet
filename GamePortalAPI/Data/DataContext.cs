using System;
using Microsoft.EntityFrameworkCore;

namespace GamePortalAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) {}
	}
}


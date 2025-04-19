namespace GamePortalAPI.Data
{
	public class DataContext : DbContext    
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) {}

		//public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentAttempt> StudentAttempts => Set<StudentAttempt>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasKey("QuestionId");

            // Configure relationships for StudentAttempt
            modelBuilder.Entity<StudentAttempt>()
	            .HasOne(a => a.Student)
	            .WithMany()
	            .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<StudentAttempt>()
	            .HasOne(a => a.Question)
	            .WithMany()
	            .HasForeignKey(a => a.QuestionId);
            
            //This will change the table name to Question, this will be used when mapping.
            //Pluralizing is the convention in Entity Framework.
            var config = modelBuilder.Entity<Question>();
            config.ToTable("Question");
            
            
        }
    }
}


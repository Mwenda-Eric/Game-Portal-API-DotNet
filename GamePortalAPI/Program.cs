
using GamePortalAPI.Repositories.StudentRepository;
using GamePortalAPI.Services.StudentService;
using GamePortalAPI.Repositories.AttemptRepository;
using GamePortalAPI.Services.AttemptService;

namespace GamePortalAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddScoped<IApiService, ApiService>();
        builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

        builder.Services.AddScoped<ISessionRepository, SessionRepository>();
        builder.Services.AddScoped<ISessionService, SessionService>();

        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<IStudentService, StudentService>();
        
        // Add new services for student attempts
        builder.Services.AddScoped<IStudentAttemptRepository, StudentAttemptRepository>();
        builder.Services.AddScoped<IStudentAttemptService, StudentAttemptService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}


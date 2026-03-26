using ExamPortal.Api.Data;
using ExamPortal.Api.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=App_Data/examportal.db";
var sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);

if (!Path.IsPathRooted(sqliteConnectionStringBuilder.DataSource))
{
    sqliteConnectionStringBuilder.DataSource = Path.Combine(
        builder.Environment.ContentRootPath,
        sqliteConnectionStringBuilder.DataSource);
}

var databaseDirectory = Path.GetDirectoryName(sqliteConnectionStringBuilder.DataSource);
if (!string.IsNullOrWhiteSpace(databaseDirectory))
{
    Directory.CreateDirectory(databaseDirectory);
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(sqliteConnectionStringBuilder.ToString()));

builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.WebHost.UseUrls("http://localhost:5001");
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbSeeder.Seed(db);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run("http://localhost:5001");

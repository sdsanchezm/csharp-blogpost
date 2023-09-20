using blogpost;
using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// map the BlogPost Service with the IBlogPost Interface
builder.Services.AddScoped<IBlogPostService, BlogPostService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection2"));
});

var app = builder.Build();

// seed the data, the command is "dotnet run initialdata"
if (args.Length == 1 && args[0].ToLower() == "initialdata")
    SeedData(app);

// this is the function that will run
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

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

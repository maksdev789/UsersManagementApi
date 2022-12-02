using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Diagnostics.Metrics;
using UsersManagement.Api.Middlewares;
using UsersManagement.Core;
using UsersManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => {

    options.UseSqlServer(@"Server=DESKTOP-UQB7GB0\SQLEXPRESS;Database=UserManagementDb;Trusted_Connection=True;TrustServerCertificate=True");

});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ApplicationDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<TokenMiddleware>(); //  склой проверки при которой  принимаемый параметр token должен быть равен "123"

app.UseAuthorization();

app.MapControllers();

app.Run();

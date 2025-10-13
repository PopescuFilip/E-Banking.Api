using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<EBankingDbContext, EBankingDbContextFactory>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<EBankingDbContext>>();

    using var dbContext = dbContextFactory.CreateDbContext();

    dbContext.Database.Migrate();

    if (!dbContext.Users.Any())
    {
        var admin = new User()
        {
            Username = "admin",
            Password = "pass",
        };

        dbContext.Users.Add(admin);
        dbContext.SaveChanges();
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

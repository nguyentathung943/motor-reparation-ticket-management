using DataAccess.DataContext;
using DataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorReparationTicketAPI.Repository;
using MotorReparationTicketAPI.Repository.IRepository;
using MotorReparationTicketAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionStringDocker")));
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IWorkItemRepository, WorkItemRepository>();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("MotorReparation", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();
ApplicationDbInitializer.Initialize(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MotorReparation");

app.UseAuthorization();

app.MapControllers();

app.Run();

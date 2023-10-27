using VigilantCore.Web.AspNet.Repository.Context;
using Microsoft.EntityFrameworkCore;
using VigilantCore.Web.AspNet.Repository;
using VigilantCore.Web.AspNet.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);

builder.Services.AddScoped<INoticeRepository, NoticeRepository>(); 
builder.Services.AddScoped<IWantedRepository, WantedRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("OpenCorsPolicy");

app.MapControllers();

app.Run();

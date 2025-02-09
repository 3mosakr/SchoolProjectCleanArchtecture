using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Core.MiddleWare;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connection to Sql Server
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

#region Dependecies injection
// Add all modules dependacies 
builder.Services.AddInfrastructureDependecies()
                .AddServiceDependecies()
                .AddCoreDependecies()
                .AddServiceRegisteration();
#endregion

#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
            new CultureInfo("en-US"),
            new CultureInfo("ar-EG")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion

#region AllowCORS
var CORS = "_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(CORS);

app.UseAuthorization();

app.MapControllers();

app.Run();

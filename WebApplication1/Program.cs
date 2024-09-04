using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Text.Json.Serialization;
using WebApplication1.DataAccess;
using WebApplication1.DomainModels;
using WebApplication1.Filters;
using WebApplication1.Middleware;
using WebApplication1.Options;
using WebApplication1.Repository;
using WebApplication1.RouteConstraints;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder();

var config = builder.Configuration;

var authenticationOptions = config.GetSection("Authentication").Get<AuthenticationInfoOption>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers(opts =>
{
    opts.ReturnHttpNotAcceptable = true;
})
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<AspContext>(opts =>
{
    opts.UseSqlServer(config["ConnectionStrings:AspContextString"]);
});

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

builder.Services.Configure<AuthenticationInfoOption>(config.GetSection("Authentication"));

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer(opts =>
//    {
//        opts.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidIssuer = authenticationOptions.Issuer,
//            ValidAudience = authenticationOptions.Audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.Secret)),

//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true
//        };
//    });

//builder.Services.AddAuthorization(opts =>
//{
//    opts.AddPolicy("mustBeZia", authzPolicy =>
//    {
//        authzPolicy.RequireClaim("userName", "ziasniper");
//    });
//});

builder.Services.AddApiVersioning(opts =>
{
    opts.ReportApiVersions = true;
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new Asp.Versioning.ApiVersion(1.0);
}).AddMvc();



var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRouting();

//app.UseAuthentication();

//app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    var path1 = endpoints.MapControllers();
});

app.Run();
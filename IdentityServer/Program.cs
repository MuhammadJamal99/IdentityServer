using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var assemply = typeof(Program).Assembly.GetName().Name;

var  defaultConnString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseNpgsql(defaultConnString, opt => opt.MigrationsAssembly(assemply))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options => 
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(defaultConnString, opt => opt.MigrationsAssembly(assemply));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(defaultConnString, opt => opt.MigrationsAssembly(assemply));
                })
                .AddDeveloperSigningCredential();

var app = builder.Build();
app.UseIdentityServer();

app.Run();

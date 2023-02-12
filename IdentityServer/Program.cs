using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SqlServer;

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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.EnsureSeedData(services);
}

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();

using IdentityServer;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace SqlServer
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            EnsureSeedData(context);
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            Console.WriteLine("Seeding database...");

            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }

            if (!context.ApiScopes.Any())
            {
                Console.WriteLine("Scopes being populated");
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Scopes already populated");
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }
    }

}
/*

*/
/*
public static void EnsureSeedData(string connectionString)
{
    var assemply = typeof(SeedData).Assembly.GetName().Name;
    var services = new ServiceCollection();
    services.AddLogging();

    services
        .AddDbContext<AspNetIdentityDbContext>(options => options.UseNpgsql(connectionString));

    services
        .AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AspNetIdentityDbContext>()
        .AddDefaultTokenProviders();

    services.AddOperationalDbContext
    (
        options =>
        {
            options.ConfigureDbContext = b => b.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(assemply));
        }
    );

    services.AddConfigurationDbContext
    (
        options =>
        {
            options.ConfigureDbContext = b => b.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(assemply));
        }
    );
    var serviceProvider = services.BuildServiceProvider();
    using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
    scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
    var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
    context.Database.Migrate();

    EnsureSeedData(context);

    var ctx = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
    ctx.Database.Migrate();
    EnsureUsers(scope);

}
private static void EnsureUsers(IServiceScope scope)
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var hala = userManager.FindByNameAsync("hala").Result;
    if (hala is null)
    {
        hala = new IdentityUser()
        {
            UserName = "hala",
            Email = "hala.sweet@gmail.com",
            EmailConfirmed = true
        };

        var result = userManager.CreateAsync(hala, "123456*").Result;
        result = userManager.AddClaimsAsync(hala,
        new Claim[]
        {
                    new Claim(JwtClaimTypes.Name, "Hala Hamed"),
                    new Claim(JwtClaimTypes.GivenName, "Hala"),
                    new Claim(JwtClaimTypes.FamilyName,"Hamed"),
                    new Claim(JwtClaimTypes.WebSite,"https://halaHamed.com"),
                    new Claim("location", "somewhere")
        }).Result;
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);
    }
}
private static void EnsureSeedData(ConfigurationDbContext context)
{
    if (!context.Clients.Any())
    {
        foreach (var client in Config.Clients.ToList())
        {
            context.Add(client.ToEntity());
        }
        context.SaveChanges();
    }

    if (!context.IdentityResources.Any())
    {
        foreach (var IdentityResource in Config.IdentityResources.ToList())
            context.Add(IdentityResource.ToEntity());

        context.SaveChanges();
    }

    if (!context.ApiScopes.Any())
    {
        foreach (var ApiScope in Config.ApiScopes.ToList())
            context.Add(ApiScope.ToEntity());

        context.SaveChanges();
    }

    if (!context.ApiResources.Any())
    {
        foreach (var ApiResource in Config.ApiResources.ToList())
            context.Add(ApiResource.ToEntity());

        context.SaveChanges();
    }
}
*/
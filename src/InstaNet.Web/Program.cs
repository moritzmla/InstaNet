using InstaNet.ApplicationCore.Entities;
using InstaNet.DataAccess.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace InstaNet.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                Profile profil = null;

                using (var repositoryContext = scope.ServiceProvider.GetService<RepositoryContext>())
                {
                    if (!(repositoryContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    {
                        await repositoryContext.Database.EnsureCreatedAsync();

                        profil = await RepositorySeed.CreateSeed(repositoryContext);

                        await repositoryContext.SaveChangesAsync();
                    }
                }
            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

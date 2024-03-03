using Microsoft.Extensions.Hosting;

namespace Wms.Tests
{
    public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                //webBuilder.UseStartup<Startup>();
            });
    }
}
}


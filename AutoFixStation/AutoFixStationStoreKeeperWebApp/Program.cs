using AutoFixStationContracts.ViewModels;

namespace AutoFixStationStoreKeeperWebApp
{
    public class Program
    {
        public static StoreKeeperViewModel StoreKeeper { get; set; }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

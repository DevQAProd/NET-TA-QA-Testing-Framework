namespace ApplicationName.Dev.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            StartUp startUp = new StartUp(builder.Configuration);
            startUp.ConfigureServices(builder.Services);
            var app = builder.Build();
            startUp.Configure(app);
        }
    }
}
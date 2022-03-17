using Microsoft.Extensions.Configuration;

public class Configuration
{
    public IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddUserSecrets<IConfiguration>()
            .AddEnvironmentVariables()
            .Build();
    }
}
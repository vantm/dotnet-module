namespace Modular;

public interface IModule
{
    void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment);
}

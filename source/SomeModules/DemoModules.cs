using Modular;

namespace SomeModules;


public abstract class NamedModule : IModule
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
    }
}


public class ModuleC : NamedModule { }

[DependsOn(typeof(ModuleE))]
public class ModuleD : NamedModule { }

public class ModuleE : NamedModule { }

[DependsOn(typeof(ModuleI))]
public class ModuleH : NamedModule { }

public class ModuleI : NamedModule { }


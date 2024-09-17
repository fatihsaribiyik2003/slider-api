using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SubuProtokol.Core
{
    public abstract class StartupBase
    {
        public IServiceCollection ServiceCollection { get; set; }
        public IConfiguration Configuration { get; set; }

        protected StartupBase(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ServiceCollection = serviceCollection;
            Configuration = configuration;
        }

        public abstract void Configure();
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubuProtokol.Core;
using SubuProtokol.Models;
using SubuProtokol.Services.EntityFramework.Abstract;
using SubuProtokol.Services.EntityFramework.Managers;
using SubuProtokol.Services.NoContext;

namespace SubuProtokol.Services
{
    public class Startup : StartupBase
    {
        public Startup(IServiceCollection serviceCollection, IConfiguration configuration) : base(serviceCollection, configuration)
        {
        }

        public override void Configure()
        {
            ServiceCollection.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
         
            #region Entity Framework

        
            ServiceCollection.AddScoped<IProtokolService, ProtokolService>();
            ServiceCollection.AddScoped<IProtokolUserService, ProtokolUserService>();
            ServiceCollection.AddScoped<IFileService, FileService>();
            ServiceCollection.AddScoped<IUnitService, UnitService>();




            #endregion
            ServiceCollection.Configure<SmtpOptions>(Configuration.GetSection("SmtpSettings"));
            ServiceCollection.AddTransient<ISmtpService, SmtpService>();
            #region 

            #endregion

            DataAccess.Startup dataAccessStartup =
                new SubuProtokol.DataAccess.Startup(ServiceCollection, Configuration);

            dataAccessStartup.Configure();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using SubuProtokol.Core;

using SubuProtokol.DataAccess.EntityFramework.Context;
using SubuProtokol.DataAccess.EntityFramework.Repositories;
using SubuProtokol.DataAccess.EntityFramework.UnitOfWork;


using SubuProtokol.Entities.Base;

namespace SubuProtokol.DataAccess
{
    // Add-Migration InitialCreate -OutputDir "EntityFramework\Migrations"

    public class Startup : StartupBase
    {
        public Startup(IServiceCollection serviceCollection, IConfiguration configuration) : base(serviceCollection, configuration)
        {
        }

        public override void Configure()
        {
            #region  Entity Framework

            ServiceCollection.AddDbContext<Databse1Context>(opts =>
         {
             opts.UseNpgsql(Configuration.GetConnectionString("PgSqlConnection"));
             opts.UseLazyLoadingProxies();
         });



            //ServiceCollection.AddScoped<IDatabase1UnitOfWork, Database1UnitOfWork>();
            ServiceCollection.AddScoped<IDatabase1UnitOfWork2, Database1UnitOfWork2>();
            ServiceCollection.AddScoped<IProtokolRepository, ProtokolRepository>();
            ServiceCollection.AddScoped<IUserProtokolRepository, UserProtokolRepository>();
            ServiceCollection.AddScoped<IUnitRepository, UnitRepository>();

            #endregion

            #region Mongo

            //BsonClassMap.RegisterClassMap<EntityBase<ObjectId>>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.SetIgnoreExtraElements(true);
            //    cm.SetIgnoreExtraElementsIsInherited(true);
            //});

            //ServiceCollection.AddScoped<MongoDBContextBase, MongoDBContext>();
            //ServiceCollection.AddScoped<IMongoCategoryRepository, MongoCategoryRepository>();
            //ServiceCollection.AddScoped<IMongoAddressRepository, MongoAddressRepository>();
            //ServiceCollection.AddScoped<IMongoUserRepository, MongoUserRepository>();

            #endregion
        }
    }
}

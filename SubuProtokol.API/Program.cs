using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SubuProtokol.API.Helpers;
using System.Text;

namespace SubuProtokol.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Host.UseSerilog((context, config) =>
            //{
            //    config.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error);

            //    string logDBConnStr = builder.Configuration.GetConnectionString("MongoDDBConnection");
            //    string logColName = builder.Configuration.GetValue<string>("AppSettings:LogCollectionName");

            //    config.WriteTo.MongoDB(logDBConnStr, logColName, Serilog.Events.LogEventLevel.Information);
            //});

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            builder.Services.AddControllers(opts =>
            {
                opts.CacheProfiles.Add("TenSecond", new CacheProfile
                {
                    Duration = 10,
                    VaryByQueryKeys = new string[] { "id" }
                });
            });
            builder.Services.AddControllers()
    .AddNewtonsoftJson();

            // Cache
            builder.Services.AddMemoryCache();
            builder.Services.AddResponseCaching();

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    string secret = builder.Configuration.GetValue<string>("AppSettings:Secret");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                    };
                });


            builder.Services.Configure<ApiBehaviorOptions>(opts =>
            {
                //opts.SuppressModelStateInvalidFilter = true;
                opts.SuppressMapClientErrors = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Swagger servisi ve swagger json üretimi için.
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp 99 API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                                }
                            },
                            new string[] {}
                }});

            });

            builder.Services.AddScoped<ITokenHelper, TokenHelper>();


            new SubuProtokol.Services.Startup(builder.Services, builder.Configuration).Configure();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{


            //}

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();
            app.UseCors("AllowLocalhost");
            app.MapControllers();

            app.Run();
        }
    }
}
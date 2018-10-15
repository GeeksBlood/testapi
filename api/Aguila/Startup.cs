using Aguila.DAL;
using Aguila.Models;
using Aguila.Repository.Interface;
using Aguila.Repository.Repository;
using Aguila.Repository.Unitofwork;
using Aguila.Service;
using Aguila.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aguila
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            ConnectionConfig.ConStr = Configuration.GetConnectionString("DefaultConnection");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                    .AllowAnyHeader()));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<ApplicationDbContextDto>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUserDto, ApplicationRoleDto>()
                .AddEntityFrameworkStores<ApplicationDbContextDto>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add framework services.
            services.AddDbContext<AguilaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IAttributeService, AttributeService>();
            services.AddTransient<IPictureBookService, PictureBookService>();
            services.AddTransient<IHandicapService, HandicapService>();
            services.AddTransient<IConfigurationsValuesService, ConfigurationsValuesService>();
            services.AddTransient<ILRDService, LRDService>();
            services.AddTransient<ISituationService, SituationService>();
            services.AddTransient<IRoundService, RoundService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ISendEmailService, SendEmailService>();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });

            //Add this for Automapper
            services.AddAutoMapper();

            //Add this for versioning header based
            //services.AddApiVersioning(o =>
            //{
            //    o.ReportApiVersions = true;
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.DefaultApiVersion = new ApiVersion(1, 0);
            //    o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Log/error_{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //Add this for static pages
            app.UseStaticFiles();

            //Add this for Authorization using Identity
            app.UseIdentity();

            //Add this ErrorHandlingMiddleware class for error logging
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            //Add this for Cross origin site
            app.UseCors("AllowAll");
           // app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            //Add this for Tokenization and Authorization
            ConfigureAuth(app);

            //Add this for default routing
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Values}/{action=Get}/{id?}");
            });
            

        }
    }
}

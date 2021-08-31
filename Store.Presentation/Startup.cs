using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Initialization;
using Store.DataAcess.StoreContext;
using Store.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.Repositories.EFRepositories;
using Newtonsoft.Json;
using Scrutor;
using Store.BusinessLogic.Options;

namespace Store.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<StripeOptions>(Configuration.GetSection("Stripe"));
            services.Configure<ApiRoutes>(Configuration.GetSection("ApiRoutes"));

            services.AddDbContext<ShopDbContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString(Shared.Constants.DefaultValues.DefaultConnection)));

            services.AddIdentity<StoreUser, IdentityRole<long>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ShopDbContext>()
                .AddDefaultTokenProviders();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Shared.Constants.DefaultValues.JwtKeyToken));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(
                    opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                        };
                    });

            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddCors();

            services.AddSwaggerGen();

            services.InitializeAsync().Wait();

            services.Scan(scan => scan
            .FromAssemblyOf<IAuthorService>()
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .AsImplementedInterfaces()
            .WithTransientLifetime()

            .FromAssemblyOf<IAuthorRepository>()
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            );

            services.AddAutoMapper(typeof(AuthorService), typeof(AuthorRepository));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseMiddleware<LoggingErrorsMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200/")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin());


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}

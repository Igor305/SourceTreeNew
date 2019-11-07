using AutoMapper;
using EducationApp.BusinessLogicLayer.AutoMapper;
using EducationApp.BusinessLogicLayer.Services;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.BusinessLogicLayer.Stripe.Infrastructure;
using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.EFRepositories;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System;
using System.IO;
using System.Text;

namespace EducationApp.PresentationLayer
{
    /// <summary>
    ///  Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connectionString = Configuration.GetSection("ConnectionStrings")["EmployeeDB"];
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(connectionString));
            services.AddIdentityCore<IdentityUser>();
            services.AddScoped<IUserStore<IdentityUser>, UserOnlyStore<IdentityUser, IdentityDbContext>>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, BusinessLogicLayer.Services.AccountService>();
            services.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();
            services.AddScoped<IPrintingEditionService, PrintingEditionService>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, BusinessLogicLayer.Services.OrderService>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<User>();
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddRoles<Role>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Education", Version = "v1" });
                var xmlFile = "Swagger.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            string key = Configuration.GetSection("JWT")["SecretKey"];
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            string jwtSchemeName = Configuration.GetSection("JWT")["JwtSchemeName"];
            services
            .AddAuthentication(options => {
                options.DefaultAuthenticateScheme = jwtSchemeName;
                options.DefaultChallengeScheme = jwtSchemeName;
            })
            .AddJwtBearer(jwtSchemeName, jwtBearerOptions => {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("JWT")["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("JWT")["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<Role> roleManager ,UserManager<User> userManager)
        {
            app.UseMiddleware<LogService>();
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Education V1");
            });
        
            app.UseAuthentication();
      
            app.UseMvc();

            MyIdentityDataInitializer.SeedData(userManager, roleManager, Configuration);
        }
    }
}

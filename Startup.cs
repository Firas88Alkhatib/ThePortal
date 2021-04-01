using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using ThePortal.Configuration;
using ThePortal.Models;
using ThePortal.Models.Facebook;
using ThePortal.Services;
using ThePortal.Services.FacebookService;
using ThePortal.Services.GoogleService;

namespace ThePortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //controllers
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection")
                ));
            //FacebookService
            services.AddHttpClient<IFacebookService, FacebookService>(c=> { 
                c.BaseAddress = new System.Uri("https://graph.facebook.com/v10.0/");
            });
            //GoogleSergvice
            services.AddHttpClient<IGoogleService, GoogleService>(c =>
            {
                c.BaseAddress = new System.Uri("");
            });
            //authService
            services.AddScoped<IAuthenticationService,AuthService>();
            //facebook api keys
            var facebookApiKeys = new FacebookApiKeys();
            facebookApiKeys.ClientId = Configuration["ApiKeys:FacebookApiKeys:ClientId"];
            facebookApiKeys.ClientSecret = Configuration["ApiKeys:FacebookApiKeys:ClientSecret"];
            services.AddSingleton<FacebookApiKeys>(facebookApiKeys);
            //jwt key
            var jwtKey = new JwtConfig()
            {
                SecretKey = Configuration["JwtConfig:SecretKey"]
            };

            services.AddSingleton<JwtConfig>(jwtKey);
            //authentication
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                var secretKey = Encoding.UTF8.GetBytes(Configuration["JwtConfig:SecretKey"]);
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                };
             });
            //identity
            services.AddIdentityCore<ApplicationUser>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.ClaimsIdentity.UserIdClaimType = "Id";
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddUserManager<ApplicationUserManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ThePortal", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThePortal v1"));
            }
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
using System.Text;
using Api.Configurations;
using Api.Domain;
using Api.Domain.Interfaces;
using Api.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api
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

            var helpetGeneralSettingsConfigSection = Configuration.GetSection(nameof(HelpetGeneralSettings));
            services.Configure<HelpetGeneralSettings>(helpetGeneralSettingsConfigSection);

            services.Configure<HelpetDBSettings>(Configuration.GetSection(nameof(HelpetDBSettings)));
            services.Configure<HelpetEmailSettings>(Configuration.GetSection(nameof(HelpetEmailSettings)));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(helpetGeneralSettingsConfigSection.Get<HelpetGeneralSettings>().JwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IHelpetGeneralSettings>(sp => sp.GetRequiredService<IOptions<HelpetGeneralSettings>>().Value);
            services.AddSingleton<IHelpetDBSettings>(sp => sp.GetRequiredService<IOptions<HelpetDBSettings>>().Value);
            services.AddSingleton<IHelpetEmailSettings>(sp => sp.GetRequiredService<IOptions<HelpetEmailSettings>>().Value);

            services.AddTransient<IFilesDomain, FilesDomain>();

            services.AddTransient<IUserDomain, UserDomain>();
            services.AddTransient<IPetDomain, PetDomain>();

            services.AddTransient<IBreedDomain, BreedDomain>();
            services.AddTransient<ICoatDomain, CoatDomain>();
            services.AddTransient<IFurColorDomain, FurColorDomain>();

            services.AddTransient<IPartnerDomain, PartnerDomain>();
            services.AddTransient<IPartnerCategoryDomain, PartnerCategoryDomain>();


            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IGooglePlaces, GooglePlaces>();

            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

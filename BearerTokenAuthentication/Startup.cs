using System;
using System.Text;
using BearerTokenAuthentication.Model;
using BearerTokenAuthentication.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BearerTokenAuthentication
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
            AddJwtBearerAuthenticationScheme(services);

            AddAuthorization(services);

            AddConfiguration(services);

            services.AddMvc();
        }

        private void AddJwtBearerAuthenticationScheme(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Token:Issuer"],
                            ValidAudience = Configuration["Token:Issuer"],
                            IssuerSigningKey = JwtSecurityKey.Create(Configuration["Token:Key"])
                        };
                    });
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                    policy => policy.RequireClaim("MembershipId"));
            });
        }

        private void AddConfiguration(IServiceCollection services)
        {
            // You can easily bind a configuration instance (or interface) explicitly without having to go through the IOptions<T> interface.
            var config = new TokenConfiguration();
            Configuration.Bind("Token", config);
            services.AddSingleton(config);

            // Use this if you want the default IOptions<TokenConfiguration> instead
            //services.Configure<TokenConfiguration>(options => Configuration.GetSection("Token").Bind(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}

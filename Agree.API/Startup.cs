using Agree.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Agree.API
{
    public class Startup
    {
        public IConfiguration config
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}

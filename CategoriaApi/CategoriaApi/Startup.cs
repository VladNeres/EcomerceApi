using CategoriaApi.Authorization;
using CategoriaApi.Data;
using CategoriaApi.Interfaces;
using CategoriaApi.Repository;
using CategoriaApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoriaApi
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
            services.AddDbContext<DatabaseContext>(opt => opt.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("CategoriaConnection")));
            services.AddScoped<CategoriaServices>();
            services.AddScoped<SubCategoriaService>();
            services.AddScoped<ProdutoServices>();
            services.AddScoped<CentroDeDistribuicaoService>();
            services.AddTransient< CategoriaRepository>();
            services.AddTransient<SubCategoriaRepository>();
            services.AddTransient<CentroRepository>();
            services.AddScoped<ICategoriaRepository,CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaServices>();
            services.AddScoped<ISubCategoriaRepository, SubCategoriaRepository>();
            services.AddScoped<ISubCategoriaService, SubCategoriaService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CategoriaApi", Version = "v1" });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IDbConnection>((sp)=> new MySqlConnection(Configuration.GetConnectionString("CategoriaConnection")));
            services.AddScoped<ProdutoRepository>();

            services.AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
             .AddJwtBearer(token =>
             {
                 token.RequireHttpsMetadata = false;
                 token.SaveToken = true;
                 token.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ClockSkew = TimeSpan.Zero
                 };
             });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("idadeMinima", policy =>
                {
                     policy.Requirements.Add(new IdadeMinimaRequirement(18));
                });
            });
            services.AddSingleton<IAuthorizationHandler, IdadeMinimaHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CategoriaApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
    
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

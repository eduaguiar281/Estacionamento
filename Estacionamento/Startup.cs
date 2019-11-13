using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.DataBase.DataContext;
using Estacionamento.Entities;
using Estacionamento.Factories;
using Estacionamento.Repositories;
using Estacionamento.Services;
using Estacionamento.UI;
using Estacionamento.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Estacionamento.ViewModel;

namespace Estacionamento
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
            services.AddControllersWithViews();
            services.AddDbContext<EstacionamentoDataContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddTransient<IDataContext, EstacionamentoDataContext>();
            services.AddTransient<IRepository<Veiculo>, EfRepository<Veiculo>>();
            services.AddTransient<IValidator<Veiculo>, VeiculoValidation>();
            services.AddTransient<IRepository<Movimentacao>, EfRepository<Movimentacao>>();
            services.AddTransient<IRepository<TabelaPreco>, EfRepository<TabelaPreco>>();
            services.AddTransient<ITabelaPrecosService, TabelaPrecoService>();
            services.AddTransient<IValidator<TabelaPreco>, TabelaPrecoValidation>();
            services.AddTransient<ITabelaPrecoViewModelFactory, TabelaPrecoViewModelFactory>();
            services.AddTransient<IValidator<Movimentacao>, MovimentacaoValidation>();
            services.AddTransient<IMovimentacaoService, MovimentacaoService>();
            services.AddTransient<IVeiculoServices, VeiculoServices>();
            services.AddTransient<IValidator<EntradaVeiculoViewModel>, EntradaVeiculoViewModelValidation>();
            services.AddTransient<IMovimentacaoVeiculoViewModelFactory, MovimentacaoVeiculoViewModelFactory>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("pt-BR"),
                new CultureInfo("pt")
            };

                options.DefaultRequestCulture = new RequestCulture("pt-BR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            /*
            services.AddMvc(config =>
            {
                ILoggerFactory loggerFactory = new LoggerFactory();
                config.ModelBinderProviders.Insert(0, new InvariantDecimalModelBinderProvider(loggerFactory));
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            serviceProvider.GetService<EstacionamentoDataContext>().Database.EnsureCreated();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using scheduler.core.Data;
using scheduler.core.Interfaces;
using scheduler.core.Respositories;
using scheduler.core.Services;
using Serilog;

namespace scheduler.api
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
            /// TODOs:
            /// [Backend]
            /// 01. [Atty] Logica de usuario (get, create ((con rol)) )
            /// 02. [Juli] Cambiar el estado de un evento
            /// 03. Asociar una lista de usuarios del tipo estudiante a un evento
            /// 04. Al crear un evento matchear el id de instructor/a con un usuario del tipo "instructor", si no existe devolver error, si existe asignar
            /// 05. Pasar api a async
            /// 06. Al cancelar un evento, escribir en consola el mail de los estudiantes involucrados
            /// 07. Definir objetos de response
            /// 08. [Claudio] Crear clase para manejar mapeos ( dto-entity || entity-dto )
            /// 09. Agregar validaciones
            /// 10. [Claudio] Manejar objetos de respuesta ante transacciones (create, put, delete)
            /// 11. Manejar excepciones
            /// 12. [Claudio] Crear collection de postman para meter datos "reales"

            services.AddSingleton(Log.Logger);
          
            Log.Logger.Information("Application Starting");
            
            services.AddControllers();

            //services.AddFluentValidation(fv =>
            //    fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidator>());

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("SchedulerDB")));

            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IEventService, EventService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "scheduler.api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "scheduler.api v1"));

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

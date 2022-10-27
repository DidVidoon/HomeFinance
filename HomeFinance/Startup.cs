using Models.Mapping;
using Services;
using Services.IRepository;

namespace Task_12
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            PresetDatabase.Configurate(Configuration, services);
            services.AddTransient<IOperationService, OperationService>();
            services.AddTransient<ITypeService, TypeService>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.Map("/error", ap => ap.Run(async context =>
            {
                await context.Response.WriteAsync("You have called a wrong adress!");
            }));

            app.UseStatusCodePages();
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            PresetDatabase.Fill(app);
        }
    }
}

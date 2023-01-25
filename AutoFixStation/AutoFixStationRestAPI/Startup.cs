using AutoFixStationBusinessLogic.BusinessLogics;
using AutoFixStationBusinessLogic.OfficePackage;
using AutoFixStationBusinessLogic.OfficePackage.Implements;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationDatabaseImplement.Implements;
using Microsoft.OpenApi.Models;

namespace AutoFixStationRestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services
        //to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Хранилища
            services.AddTransient<IEmployeeStorage, EmployeeStorage>();
            services.AddTransient<IStoreKeeperStorage, StoreKeeperStorage>();
            services.AddTransient<ICarStorage, CarStorage>();
            services.AddTransient<IServiceRecordStorage, ServiceRecordStorage>();
            services.AddTransient<ISparePartStorage, SparePartStorage>();
            services.AddTransient<ITimeOfWorkStorage, TimeOfWorkStorage>();
            services.AddTransient<ITOStorage, TOStorage>();
            services.AddTransient<IWorkStorage, WorkStorage>();
            services.AddTransient<IWorkTypeStorage, WorkTypeStorage>();

            //Логика
            services.AddTransient<IEmployeeLogic, EmployeeLogic>();
            services.AddTransient<IStoreKeeperLogic, StoreKeeperLogic>();
            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<IServiceRecordLogic, ServiceRecordLogic>();
            services.AddTransient<ISparePartLogic, SparePartLogic>();
            services.AddTransient<ITimeOfWorkLogic, TimeOfWorkLogic>();
            services.AddTransient<ITOLogic, TOLogic>();
            services.AddTransient<IWorkLogic, WorkLogic>();
            services.AddTransient<IWorkTypeLogic, WorkTypeLogic>();
            services.AddTransient<IReportLogic, ReportLogic>();
            services.AddTransient<AbstractSaveToWord, SaveToWord>();
            services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
            services.AddTransient<AbstractSaveToPdf, SaveToPdf>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AutoFixStationRestAPI",
                    Version = "v1"
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the
        //HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoFixStationRestApi v1"));
            }
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

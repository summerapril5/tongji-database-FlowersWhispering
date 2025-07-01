using FlowersWhisperingAPI.Administrator.Mappers;
using FlowersWhisperingAPI.Administrator.Services;
using FlowersWhisperingAPI.Administrator.Services.Interface;

namespace FlowersWhisperingAPI.Administrator
{
    public static class AdministratorServiceRegistration
    {
        public static IServiceCollection AddAdminService(this IServiceCollection services, string connectionString)
        {
            //services.AddSingleton(new AccountMapper(connectionString));
            
            // 注册自定义服务
            services.AddSingleton(new AdminReviewMapper(connectionString));
            services.AddSingleton(new AdminAnnoMapper(connectionString));
            services.AddSingleton(new AdminUserMapper(connectionString));
            services.AddScoped<IAdminReviewService, AdminReviewService>();
            services.AddScoped<IAdminAnnoService, AdminAnnoService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
           
            // 可以在这里继续注册其他服务
            // services.AddScoped<IOtherService, OtherService>();

            return services;
        }
    }
}
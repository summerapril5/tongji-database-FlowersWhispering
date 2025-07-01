using FlowersWhisperingAPI.Community.Mappers;
using FlowersWhisperingAPI.Community.Services;
using FlowersWhisperingAPI.Community.Services.Interface;

namespace FlowersWhisperingAPI.Community
{
    public static class CommunityServiceRegistration
    {
        public static IServiceCollection AddCommunityService(this IServiceCollection services, string connectionString)
        {
            
            // 注册自定义服务
            services.AddSingleton(new CommuniPostsandCommentsMapper(connectionString));
            services.AddSingleton(new CommuniScoreMapper(connectionString));
            services.AddSingleton(new CommuniFavorMapper(connectionString));
            services.AddScoped<ICommuniPostsandCommentsService, CommuniPostsandCommentsService>();
            services.AddScoped<ICommuniScoreService, CommuniScoreService>();
            services.AddScoped<ICommuniFavorService, CommuniFavorService>();

            // 可以在这里继续注册其他服务
            // services.AddScoped<IOtherService, OtherService>();

            return services;
        }
    }
}
namespace WatermarkService.Worker.Extensions;

public static class HostedServices
{
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();

        return services;
    }
}
using BeeClient.Client.Data;
using BeeClient.Client.Helpers;
using Blazored.LocalStorage;

namespace BeeClient.Client.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCommonServices(this IServiceCollection services)
    {
        string ServerAddres = @"http://213.171.4.235:";
        services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri(ServerAddres)
            });
        services.AddScoped<IAlertService, AlertService>();

        services.AddScoped<JavascriptHelper>();
        services.AddScoped<LogWriter>();
        services.AddScoped<DataSender>();
        services.AddScoped<TokenData>();
        services.AddScoped<UserData>();
        services.AddScoped<CompanyData>();
        services.AddScoped<CompanyData>(provider => new CompanyData(provider.GetRequiredService<DataSender>(), ServerAddres));

        services.AddBlazoredLocalStorage();
    }
}

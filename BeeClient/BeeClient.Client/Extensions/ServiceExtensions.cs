using BeeClient.Client.Data;
using BeeClient.Client.Helpers;
using Blazored.LocalStorage;

namespace BeeClient.Client.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCommonServices(this IServiceCollection services)
    {
        services.AddScoped(sp => new HttpClient());
        services.AddScoped<IAlertService, AlertService>();

        services.AddScoped<JavascriptHelper>();
        services.AddScoped<LogWriter>();
        services.AddScoped<DataSender>();
        services.AddScoped<TokenData>();
        services.AddScoped<UserData>();
        services.AddScoped<CompanyData>();

        services.AddBlazoredLocalStorage();
    }
}

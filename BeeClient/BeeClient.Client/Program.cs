using BeeClient.Client.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);


        //di
        builder.Services.ConfigureCommonServices();

        builder.Services.AddBlazoredLocalStorage();
        var app = builder.Build();


        await app.RunAsync();
    }
}



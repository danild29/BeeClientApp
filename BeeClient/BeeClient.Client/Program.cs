using BeeClient.Client.Data;
using BeeClient.Client.Extensions;
using BeeClient.Client.Helpers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


//di
builder.Services.ConfigureCommonServices();




builder.Services.AddBlazoredLocalStorage();
var app = builder.Build();


await app.RunAsync();

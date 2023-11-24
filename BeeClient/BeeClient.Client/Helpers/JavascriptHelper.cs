using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BeeClient.Client.Helpers;

public class JavascriptHelper
{
    public JavascriptHelper(IJSRuntime jSRuntime)
    {
        JSRun = jSRuntime;
    }

    public IJSRuntime JSRun { get; }

    public async Task LogInfo<T>(T info) =>
        await LogInfo(JsonConvert.SerializeObject(info));

    public async Task LogInfo(string info)
    {
        await JSRun.InvokeVoidAsync("MyLogger", info);
    }
}

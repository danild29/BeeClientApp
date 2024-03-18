//using BeeClient.Client.Data.Logs;
using BeeClient.Client.Entities.Models;
using BeeClient.Client.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BeeClient.Client.Data;

public class DataSender
{
    private readonly HttpClient client;

    private readonly LogWriter logWriter;
    private readonly ILogger<DataSender> logger;

    public DataSender(HttpClient client, LogWriter logWriter, ILogger<DataSender> logger)
    {
        this.client = client;
        this.logWriter = logWriter;
        this.logger = logger;
    }


    //private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    public async Task<Result<TRes>> Post<TRes, TData>(TData info, string url)
    {
        string json = JsonConvert.SerializeObject(info);


        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        LoggerExttensions.Logger.Information(url);
        LoggerExttensions.Logger.Information(JsonConvert.SerializeObject(data));
        HttpResponseMessage response;
        try
        {
            response = await client.PostAsync(url, data);
        }
        catch (Exception ex)
        {
            throw;
        }
        LoggerExttensions.Logger.Information(JsonConvert.SerializeObject(response));

        string result = await response.Content.ReadAsStringAsync();


        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<TRes>(result);
        }

        await logWriter.WriteToConsole(result);
        return Result.Failure<TRes>(await Error.Create(response));
    }


    public async Task<Result<TRes>> Get<TRes>(string url, string? token = null)
    {
        if (token is not null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.GetAsync(url);
        await logWriter.WriteToConsole(response);

        LoggerExttensions.Logger.Information(url);
        string result = await response.Content.ReadAsStringAsync();
        LoggerExttensions.Logger.Information(JsonConvert.SerializeObject(response));

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<TRes>(result);
        }

        await logWriter.WriteToConsole(result);
        return Result.Failure<TRes>(await Error.Create(response));
    }


    //private async Task ManageToken(TokenModel tokens)
    //{
    //    if(tokens is null)
    //    {

    //    }
    //    else
    //    {
    //        if (!JwtTokenHelper.IsTokenExpired(tokens.access))
    //        {
    //        }
    //        else if (!JwtTokenHelper.IsTokenExpired(tokens.refresh))
    //        {
    //            var res = await Post<string, dynamic>(new { tokens.refresh }, "token/refresh/");
    //            tokens.access = res;
    //        }
    //        else
    //        {

    //        }
    //        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.access);
    //    }
    //}
}

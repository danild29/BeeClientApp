using BeeClient.Client.Data.Logs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BeeClient.Client.Data;

public class DataSender
{
    public readonly HttpClient _client;

    private readonly LogWriter _logWriter;

    public DataSender(HttpClient client, LogWriter logWriter)
    {
        _client = client;
        _logWriter = logWriter;
    }


    //private JsonSerializerOptions CaseInsensitive = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    public async Task<TRes> Post<TRes, TData>(TData info, string url)
    {
        string json = JsonConvert.SerializeObject(info);


        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(url, data);
        await _logWriter.WriteToLogs(response);

        string result = await response.Content.ReadAsStringAsync();


        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<TRes>(result);
        }

        throw new Exception(result);
    }


    public async Task<TRes> Get<TRes>(string url, string? token = null)
    {
        if (token is not null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await _client.GetAsync(url);
        await _logWriter.WriteToLogs(response);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<TRes>(result);
        }

        throw new Exception(result);
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

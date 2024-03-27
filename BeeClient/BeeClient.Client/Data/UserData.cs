using BeeClient.Client.Entities.Models;
using BeeClient.Client.Entities.Models.DTos;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BeeClient.Client.Data;


public class UserData
{
    private readonly static string address = "http://213.171.4.235:8081/api/users/";

    private string tokenKey = nameof(TokenModel);
    private string accountKey = nameof(UserAccount);
    private string rememberKey = "remember";


    public UserData(DataSender sender, TokenData tokenData, ILocalStorageService storage)
    {
        TokenData = tokenData;
        _sender = sender;
        _storage = storage;
    }

    public readonly TokenData TokenData;
    private readonly DataSender _sender;
    private readonly ILocalStorageService _storage;

    public async Task<Result<UserModel>> RegisterWIthStorage(CreateUser user)
    {
        Result<UserModel>? registered = await Register(user);
        if(registered.IsFailure)
        {
            return registered;
        }
        await _storage.SetItemAsync(accountKey, new UserAccount(user.Email, user.Password));
        return registered;
    }

    public async Task<Result<UserModel>> GetMeFromStorage()
    {
        UserAccount? account = await _storage.GetItemAsync<UserAccount>(accountKey);
        if (account == null) return null;


        Result<TokenModel>? resultTokens = await TokenData.GetToken(account);
        if(resultTokens.IsFailure)
        {
            return Result.Failure<UserModel>( resultTokens.Error);
        }

        
        await _storage.SetItemAsync(tokenKey, resultTokens.Value);


        return await GetMe(resultTokens.Value.access);
    }

    public async Task<Result<UserModel>> CheckIfAlredyLogged()
    {
        bool? saved = await _storage.GetItemAsync<bool?>(rememberKey);
        if(saved == true)
        {
            return await GetMeFromStorage();
        }
        return Result.Failure<UserModel>(Error.None);
    }
    public async Task ToggleRemember(bool save)
    {
        await _storage.SetItemAsync(rememberKey, save);

    }



    public async Task<Result<UserModel>> Register(CreateUser user)
    {
        return await _sender.Post<UserModel, CreateUser>(user, address);
    }
    
    public async Task<Result<UserModel>> GetMe(string token)
    {
        Result<DataMe>? result =  await _sender.Get<DataMe>(address + "me/", token);
        if(result.IsFailure)
        {
            return Result.Failure<UserModel>(result.Error);
        }

        return result.Value.Data;
    }
    
    
    public async Task<Result<UserModel>> GetUser(int id, string token)
    {
        return await _sender.Get<UserModel>(address + id.ToString(), token);
    }


}

public class DataMe
{
    public UserModel Data { get; set; }
}
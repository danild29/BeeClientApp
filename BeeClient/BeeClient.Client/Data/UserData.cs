using BeeClient.Client.Entities.Models;
using BeeClient.Client.Entities.Models.DTos;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BeeClient.Client.Data;


public class UserData
{
    private readonly static string address = "users/";

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

    public async Task<UserModel> RegisterWIthStorage(CreateUser user)
    {
        var registered = await Register(user);
        await _storage.SetItemAsync(accountKey, new UserAccount(user.Email, user.Password));
        return registered;
    }

    public async Task<UserModel?> GetMeFromStorage()
    {
        var account = await _storage.GetItemAsync<UserAccount>(accountKey);
        if (account == null) return null;


        var tokens = await TokenData.GetToken(account);
        if (tokens == null) return null;
        await _storage.SetItemAsync(tokenKey, tokens);


        return await GetMe(tokens.access);
    }

    public async Task<UserModel?> CheckIfAlredyLogged()
    {
        bool? saved = await _storage.GetItemAsync<bool?>(rememberKey);
        if(saved == true)
        {
            return await GetMeFromStorage();
        }
        return null;
    }
    public async Task ToggleRemember(bool save)
    {
        await _storage.SetItemAsync(rememberKey, save);

    }



    public async Task<UserModel> Register(CreateUser user)
    {
        return await _sender.Post<UserModel, CreateUser>(user, address);
    }
    
    public async Task<UserModel> GetMe(string token)
    {
        var data =  await _sender.Get<DataMe>(address + "me/", token);
        return data.Data;
    }
    
    
    public async Task<UserModel> GetUser(int id, string token)
    {
        return await _sender.Get<UserModel>(address + id.ToString(), token);
    }


}

public class DataMe
{
    public UserModel Data { get; set; }
}
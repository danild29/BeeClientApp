using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BeeClient.Client.Entities.Models;

public class UserModel
{
    [JsonProperty(PropertyName = "id")] public int Id{ get; set; }
    [JsonProperty(PropertyName = "email")] public string Email { get; set; }
    
    [JsonProperty(PropertyName = "first_name")] public string FirstName { get; set; }
    
    [JsonProperty(PropertyName = "last_name")] public string LastName { get; set; }

    [JsonProperty(PropertyName = "password")] public string Password { get; set; }
}



public class UserAccount
{
    public UserAccount(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required][JsonProperty(PropertyName = "email")][Display(Name = "email")] 
    public string Email { get; set; }

    [Required][JsonProperty(PropertyName = "password")][Display(Name = "пароль")] 
    public string Password { get; set; }
}
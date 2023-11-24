using System.ComponentModel.DataAnnotations;

namespace BeeClient.Client.Entities.Models.DTos;

public class CreateUser
{
    [Required][EmailAddress][Display(Name = "email")] public string Email { get; set; }

    [Required][Display(Name = "имя")] public string FirstName { get; set; }

    [Required][Display(Name = "фамилия")] public string LastName { get; set; }

    [Required][Display(Name = "пароль")] public string Password { get; set; }
}

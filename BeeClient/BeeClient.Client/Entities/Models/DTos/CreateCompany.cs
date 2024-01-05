using System.ComponentModel.DataAnnotations;

namespace BeeClient.Client.Entities.Models.DTos;

public class CreateCompany
{
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][Display(Name = "Название")] public string Name { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][Display(Name = "Тип деятельности 1")] public string WorkType1 { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][Display(Name = "Тип деятельности 2")] public string WorkType2 { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][Display(Name = "Описание")] public string Description { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][Display(Name = "ИНН")][RegularExpression(@"\d{12}|\d{10}", ErrorMessage = "Неверный формат ИНН: ИНН состоит из 10 или 12 цифр")] public string INN { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][Display(Name = "Телефон")][RegularExpression(@"^\+?\d{1,3}[-]?\d{3}[-]?\d{3}[-]?\d{4}$", ErrorMessage = "Неверный формат номера телефона")] public string Phone { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][EmailAddress][Display(Name = "email")] public string Email { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")][Display(Name = "Адрес")] public string Adress { get; set; }

}
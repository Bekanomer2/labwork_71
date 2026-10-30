using System.ComponentModel.DataAnnotations;

namespace labwork_71.ViewModels;

public class OrderCreateViewModel
{
    public int UserId { get; set; }

    public int CafeId { get; set; }

    [Required(ErrorMessage = "Введите имя получателя")]
    [Display(Name = "Имя получателя")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 100 символов")]
    public string? RecipientName { get; set; } 

    [Required(ErrorMessage = "Введите номер телефона")]
    [Display(Name = "Номер телефона")]
    [Phone(ErrorMessage = "Некорректный формат номера телефона")]
    public string? RecipientPhone { get; set; } 

    [Required(ErrorMessage = "Введите адрес доставки")]
    [Display(Name = "Адрес доставки")]
    [StringLength(250, MinimumLength = 5, ErrorMessage = "Адрес доставки должен содержать от 5 до 250 символов")]
    public string? DeliveryAddress { get; set; } 
}

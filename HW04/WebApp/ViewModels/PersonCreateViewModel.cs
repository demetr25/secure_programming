using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class PersonCreateViewModel
{
    [MaxLength(128)] 
    public string PersonName { get; set; }
}
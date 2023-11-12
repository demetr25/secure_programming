using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class PersonIndexViewModel
{
    public Guid Id { get; set; }
    
    [MaxLength(128)] 
    public string PersonName { get; set; }
}
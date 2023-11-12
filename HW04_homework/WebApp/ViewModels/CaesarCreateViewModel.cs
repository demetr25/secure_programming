using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class CaesarCreateViewModel
{
    [MaxLength(128)]
    public string Plaintext { get; set; }
    
    [RegularExpression("^(-?[1-9]\\d*)$", ErrorMessage = "Value must be a non-zero integer.")]
    public int CypherKey { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class VigenereCreateViewModel
{
    [MaxLength(128)]
    public string Plaintext { get; set; }
    public string CypherKey { get; set; }
}
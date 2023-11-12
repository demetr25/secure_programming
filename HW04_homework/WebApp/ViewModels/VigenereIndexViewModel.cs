using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class VigenereIndexViewModel
{
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string Plaintext { get; set; }
    public string CypherKey { get; set; }
    public string? Cyphertext { get; set; }
}
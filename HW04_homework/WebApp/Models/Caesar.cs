using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Caesar
{
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string Plaintext { get; set; }
    
    [RegularExpression("^(-?[1-9]\\d*)$", ErrorMessage = "Value must be a non-zero integer.")]
    public int CypherKey { get; set; }
    public string? Cyphertext { get; set; }
    
    [MaxLength(64)]
    public string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
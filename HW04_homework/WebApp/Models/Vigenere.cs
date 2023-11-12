using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Vigenere
{
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string Plaintext { get; set; }
    public string CypherKey { get; set; }
    public string? Cyphertext { get; set; }
    
    [MaxLength(64)]
    public string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
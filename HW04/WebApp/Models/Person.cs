using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Models;

public class Person
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string PersonName { get; set; }
    
    public ICollection<Contact>? Contacts { get; set; }
    
    [MaxLength(64)]
    public string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}

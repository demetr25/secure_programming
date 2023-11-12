using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

public class AppUser : IdentityUser
{
    public ICollection<Person>? Persons { get; set; }
    public ICollection<ContactType>? ContactTypes { get; set; }
}
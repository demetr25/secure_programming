using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Contact
{
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string ContactValue { get; set; }
    
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }
    
    public Guid ContactTypeId { get; set; }
    public ContactType? ContactType { get; set; }
}
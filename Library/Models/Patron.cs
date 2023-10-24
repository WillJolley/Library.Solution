using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace Library.Models
{
  public class Patron : ApplicationUser
  {
    public string Name { get; set; }
    public int PatronId { get; set; }
    public List<Checkout> JoinEntities { get; }
    // public List<Copy> Copies { get; set; }
    // public List<AuthorBook> JoinEntities { get; }
  }
}
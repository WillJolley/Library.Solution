using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace Library.Models
{
  public class Librarian : ApplicationUser
  {
    public string Name { get; set; }
    
    public int LibrarianId { get; set; }
    // public List<Copy> Copies { get; set; }
    // public List<AuthorBook> JoinEntities { get; }
  }
}
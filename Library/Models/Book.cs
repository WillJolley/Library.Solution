using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace Library.Models
{
  public class Book
  {
    public string Title { get; set; }
    public int BookId { get; set; }
    public List<Copy> Copies { get; set; }
    public List<AuthorBook> JoinEntities { get; }
  }
}
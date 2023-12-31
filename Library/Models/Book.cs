using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System.Collections;

namespace Library.Models
{
  public class Book
  {
    public string Title { get; set; }
    public int BookId { get; set; }
    
    public DateOnly DueDate { get; set; }
    public List<Copy> Copies { get; set; }
    public List<AuthorBook> JoinEntities { get; }
  }
}
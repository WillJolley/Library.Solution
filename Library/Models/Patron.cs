using System;
using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public int PatronId { get; set; }
    public string Name { get; set; }
    public DateOnly Birthdate { get; set; }
    // public List<Checkout> JoinEntities { get; }
    public List<Copy> Checkouts { get; set; }
  }
}
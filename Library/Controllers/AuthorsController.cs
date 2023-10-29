using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  [Authorize]
  public class AuthorsController : Controller
  {
    private readonly LibraryContext _db;

    public AuthorsController(LibraryContext db)
    {
      _db = db;
    }
    
    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Authors.ToList());
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Author thisAuth = _db.Authors
          .Include(author => author.JoinEntities)
          .ThenInclude(join => join.Book)
          .FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuth);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Author auth)
    {
      _db.Authors.Add(auth);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      Author thisAuth = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisAuth);
    }

    [HttpPost]
    public ActionResult AddBook(Author auth, int bookId)
    {
      #nullable enable
      AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => (join.BookId == bookId && join.AuthorId == auth.AuthorId));
      #nullable disable
      if (joinEntity == null && bookId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { BookId = bookId, AuthorId = auth.AuthorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = auth.AuthorId });
    }

    public ActionResult Edit(int id)
    {
      Author thisAuth = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      return View(thisAuth);
    }

    [HttpPost]
    public ActionResult Edit(Author auth)
    {
      _db.Authors.Update(auth);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Author thisAuth = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      return View(thisAuth);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Author thisAuth = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
      _db.Authors.Remove(thisAuth);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      AuthorBook joinEntry = _db.AuthorBooks.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBooks.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
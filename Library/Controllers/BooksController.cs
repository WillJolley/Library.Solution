using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // public async Task<ActionResult> Index()
        // {
        // string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        // List<Books> userItems = _db.Items
        //                     .Where(entry => entry.User.Id == currentUser.Id)
        //                     .Include(item => item.Category)
        //                     .ToList();
        // return View(userItems);
        // }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString)
        {
            if (_db.Books == null)
            {
                return Problem("Entity set 'Library.Books'  is null.");
            }

            var books = from m in _db.Books
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title!.Contains(searchString));
            }

            return View(await _db.Books.ToListAsync());
        }

        // public ActionResult Index()
        // {
        // return View(_db.Books.ToList());
        // }

        public ActionResult Create()
        {
            // ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                // ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
                return View(book);
            }
            else
            {
                // string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
                // book.User = currentUser;
                _db.Books.Add(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
        Book thisBook = _db.Books
                            // .Include(book => book.Author)
                            .Include(book => book.JoinEntities)
                            .ThenInclude(join => join.Author)
                            .Include(book => book.Copies)
                            .FirstOrDefault(book => book.BookId == id);
        return View(thisBook);
        }
        
        public ActionResult Edit(int id)
        {
            Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
            // ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
            return View(thisBook);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            _db.Books.Update(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
            return View(thisBook);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
            _db.Books.Remove(thisBook);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddAuthor(int id)
        {
        Book thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
        return View(thisBook);
        }

        [HttpPost]
        public ActionResult AddAuthor(Book book, int authorId)
        {
        #nullable enable
        AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => (join.AuthorId == authorId && join.BookId == book.BookId));
        #nullable disable
        if (joinEntity == null && authorId != 0)
        {
            _db.AuthorBooks.Add(new AuthorBook() { AuthorId = authorId, BookId = book.BookId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = book.BookId });
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

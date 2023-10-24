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

        public ActionResult Index()
        {
        return View(_db.Books.ToList());
        }

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

        public ActionResult Details(int id)
        {
        Book thisBook = _db.Books
                            // .Include(book => book.Author)
                            .Include(book => book.JoinEntities)
                            .ThenInclude(join => join.Author)
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

        // public ActionResult AddTag(int id)
        // {
        // Book thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        // ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
        // return View(thisBook);
        // }

        // [HttpPost]
        // public ActionResult AddTag(Book book, int tagId)
        // {
        // #nullable enable
        // ItemTag? joinEntity = _db.ItemTags.FirstOrDefault(join => (join.TagId == tagId && join.ItemId == item.ItemId));
        // #nullable disable
        // if (joinEntity == null && tagId != 0)
        // {
        //     _db.ItemTags.Add(new ItemTag() { TagId = tagId, ItemId = item.ItemId });
        //     _db.SaveChanges();
        // }
        // return RedirectToAction("Details", new { id = item.ItemId });
        // }

    //     [HttpPost]
    //     public ActionResult DeleteJoin(int joinId)
    //     {
    //         ItemTag joinEntry = _db.ItemTags.FirstOrDefault(entry => entry.ItemTagId == joinId);
    //         _db.ItemTags.Remove(joinEntry);
    //         _db.SaveChanges();
    //         return RedirectToAction("Index");
    //     }
    // }

}
}
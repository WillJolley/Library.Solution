// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Library.Models;
// using Library.ViewModels;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;

// namespace Library.Controllers {
//   public class LibrariansController : Controller
//   {

//     private readonly LibraryContext _db;

//     private readonly UserManager<ApplicationUser> _userManager;
//     private readonly SignInManager<ApplicationUser> _signInManager;

//     public LibrariansController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, LibraryContext db)
//     {
//       _userManager = userManager;
//       _signInManager = signInManager;
//       _db = db;
//     }

//     public ActionResult Index()
//     {
//       return View();
//     }

//     public ActionResult Register()
//     {
//       return View();
//     }

//     [HttpPost]
//     public async Task<ActionResult> Register (RegisterViewModel model)
//     {
//       if (!ModelState.IsValid)
//       {
//         return View(model);
//       }
//       else
//       {
//       Librarian librarian = new Librarian { UserName = model.Email };
//       IdentityResult result = await _userManager.CreateAsync(librarian, model.Password);
//       if (result.Succeeded)
//       {
//         return RedirectToAction("Index");
//       }
//       else
//       {
//         foreach (IdentityError error in result.Errors)
//         {
//           ModelState.AddModelError("", error.Description);
//         }
//         return View();
//         }
//       }
//     }

//     public ActionResult Login()
//     {
//       return View();
//     }

//     [HttpPost]
//     public async Task<ActionResult> Login(LoginViewModel model)
//     {
//       var user = await _userManager.FindByNameAsync(model.Email);
//       if (user != null)
//       {
//         var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: false);
//         if (signInResult.Succeeded)
//         {
//           return RedirectToAction("Index");
//         }
//       }
//       ModelState.AddModelError("", "Invalid username or password");
//       return View();
//     }

//     [HttpPost]
//     public async Task<ActionResult> LogOff()
//     {
//       await _signInManager.SignOutAsync();
//       return RedirectToAction("Index");
//     }
//   }
// }

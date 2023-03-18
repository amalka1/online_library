using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Web;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Online_Library.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;
    public HomeController(IWebHostEnvironment webHostEnvironment, IWebHostEnvironment environment, ILogger<HomeController> logger, MyContext context)
    {
        _webHostEnvironment = webHostEnvironment;
        _environment = environment;
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        Console.WriteLine("we are at index");
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("Register");
        }
        // ViewBag.allbooks = _context.Books.Include(e => e.bookCreator).ToList();
        ViewBag.allusers = _context.Users.ToList();
        ViewBag.id = (int)HttpContext.Session.GetInt32("userId");
        int id = (int)HttpContext.Session.GetInt32("userId");
        var userinuse = _context.Users.First(e => e.userId == id);
        ViewBag.UserName = userinuse.firstName;
        ViewBag.allbooks = _context.Books.Include(e => e.bookCreator).ToList();
        return RedirectToAction("Dashboard");
    }
    [HttpPost("/File/Upload")]
    public ActionResult Upload(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, ("~/Images/"), fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
        }

        return RedirectToAction("Dashboard");
    }

    [HttpPost("/addComment/{bookid}")]
    public IActionResult addComment(Comment comment, int bookid)
    {
        int loggeduser = (int)HttpContext.Session.GetInt32("userId");
        comment.userId = loggeduser;
        comment.bookId = bookid;
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }
    [HttpPost("/addReview/{bookid}")]
    public IActionResult addReview(Review review, int bookid)
    {
        int loggeduser = (int)HttpContext.Session.GetInt32("userId");
        Review? theReview = _context.Reviews.FirstOrDefault(e => e.bookId == bookid && e.userId == loggeduser);
        if (theReview == null)
        {
            review.userId = loggeduser;
            review.bookId = bookid;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            //  return RedirectToAction ("Dashboard");
        }
        return RedirectToAction("Dashboard");
    }
    [HttpGet("/likeBook/{userid}/{bookid}")]
    public IActionResult likeBook(int userid, int bookid)
    {
        Console.WriteLine("we are at /likeBook/{userid}/{bookid}");
        ViewBag.listlikedbook = _context.Likes.Include(e => e.user).Where(e => e.bookId == bookid).ToList();
        //ViewBag.likedbook = ViewBag.listlikedbook.Count();
        Console.WriteLine("This is the online library" + ViewBag.likedbook + " book id is equal to " + bookid);
        Like? liku1 = _context.Likes.FirstOrDefault(e => e.bookId == bookid && e.userId == userid);
        if (liku1 == null)
        {
            Like liku = new Like();
            liku.bookId = bookid;
            liku.userId = userid;
            _context.Books.Include(e => e.booklikes);
            _context.Likes.Add(liku);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", new { bookid = bookid });
        }
        return RedirectToAction("Dashboard", new { bookid = bookid });
    }
    [HttpPost("/likeBook/Api")]
    public IActionResult likeBookApi([FromBody] string idPost)
    {

        int idINT = Int32.Parse(idPost);
        int loggeduser = (int)HttpContext.Session.GetInt32("userId");
        // Console.WriteLine("we are at /likeBook/{userid}/{bookid}");
        // ViewBag.listlikedbook = _context.Likes.Include(e => e.user).Where(e => e.bookId == bookid).ToList();
        //ViewBag.likedbook = ViewBag.listlikedbook.Count();
        // Console.WriteLine("This is the online library" + ViewBag.likedbook + " book id is equal to " + bookid);
        Like? liku1 = _context.Likes.FirstOrDefault(e => e.bookId == idINT && e.userId == loggeduser);
        if (liku1 == null)
        {
            Like liku = new Like();
            liku.bookId = idINT;
            liku.userId = loggeduser;
            _context.Books.Include(e => e.booklikes);
            _context.Likes.Add(liku);
            _context.SaveChanges();
            int nrLikeve = _context.Likes.Where(e => e.bookId == idINT).Count();

            return Json(nrLikeve);
        }else
        {
            Like? liku = _context.Likes.First(e => e.bookId == idINT && e.userId == loggeduser);

            // _context.Books.Include(e => e.booklikes);
            _context.Likes.Remove(liku);
            _context.SaveChanges();
            int nrLikeve = _context.Likes.Where(e => e.bookId == idINT).Count();

        return Json(nrLikeve);
        }
        // return RedirectToAction("Dashboard", new { bookid = bookid });
    }
    [HttpPost("/unlikeBook/Api")]
    public IActionResult unlikeBookApi([FromBody] string idPost)
    {

        int idINT = Int32.Parse(idPost);
        // Console.WriteLine("we are at /likeBook/{userid}/{bookid}");
        // ViewBag.listlikedbook = _context.Likes.Include(e => e.user).Where(e => e.bookId == bookid).ToList();
        //ViewBag.likedbook = ViewBag.listlikedbook.Count();
        // Console.WriteLine("This is the online library" + ViewBag.likedbook + " book id is equal to " + bookid);
        // Like? liku1 = _context.Likes.FirstOrDefault(e => e.bookId == bookid && e.userId == userid);
        // if (liku1 == null)
        // {


        int loggeduser = (int)HttpContext.Session.GetInt32("userId");
        Like? liku = _context.Likes.First(e => e.bookId == idINT && e.userId == loggeduser);

        // _context.Books.Include(e => e.booklikes);
        _context.Likes.Remove(liku);
        _context.SaveChanges();
        int nrLikeve = _context.Likes.Where(e => e.bookId == idINT).Count();

        return Json(nrLikeve);
        // }
        // return RedirectToAction("Dashboard", new { bookid = bookid });
    }
    [HttpGet("/Delete/{id}")]
    public IActionResult delete(int id)
    {
        Book modeli = _context.Books.First(e => e.bookId == id);
        var path = Path.Combine(_webHostEnvironment.WebRootPath, ("BookImages/"), modeli.ImagePath);
        FileInfo file = new FileInfo(path);
        file.Delete();
        _context.Books.Remove(modeli);
        _context.SaveChanges();
        return RedirectToAction("yourspace");
    }
    [HttpGet("/edit/{id}")]
    public IActionResult edit(int id)
    {
        ViewBag.allCategorys = _context.Categorys.ToList();
        Book book = _context.Books.Find(id);
        return View(book);
    }
    [HttpGet("category/{id}")]
    public IActionResult category(int id)
    {
        ViewBag.booksincategory = _context.Books.Where(e => e.categoryId == id).ToList();
        ViewBag.listlikedbook = _context.Likes.Include(e => e.user).ToList();
        //this takes all books including users
        ViewBag.allbooks = _context.Books.Include(e => e.bookCreator).ToList();
        //this is the id of logged user
        ViewBag.id = (int)HttpContext.Session.GetInt32("userId");
        //this takes all the comments 
        ViewBag.allcomments = _context.Comments.Include(e => e.user).ToList();
        //this takes all the reviews 
        ViewBag.allreviews = _context.Reviews.ToList();
        ViewBag.likesinbook = _context.Books.Include(e => e.booklikes).ToList();
        Category cat = _context.Categorys.First(e => e.categoryId == 1);
        return View();
    }
    [HttpPost("/edit/{id}")]
    public async Task<IActionResult> edit(Book bookfromview, int id)
    {
        Console.WriteLine("We are at edit.");
        if (ModelState.IsValid)
        {
            Console.WriteLine("ModelState is valid ");
            //This lines of code manage to delete the cover associated with the book that we are updating.
            Book bookindb = _context.Books.First(e => e.bookId == id);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "BookImages", bookindb.ImagePath);
            FileInfo file = new FileInfo(path);
            file.Delete();
            //This part takes in charge to reupload the file(that is an image) taken from the form
            if (bookfromview.file != null && bookfromview.file.Length > 0)
            {
                Console.WriteLine("We are about to upload the new file");
                var fileName = Path.GetFileName(bookfromview.file.FileName);
                Console.WriteLine("This is the file name " + fileName);
                var fileExtension = Path.GetExtension(fileName);
                Console.WriteLine("This is the file extension " + fileExtension);
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "BookImages", uniqueFileName);
                Console.WriteLine("This is the file path " + filePath);
                // Save the image file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Console.WriteLine("We are at using thing ");
                    await bookfromview.file.CopyToAsync(stream);
                }
                bookfromview.ImagePath = uniqueFileName;
                Console.WriteLine("This is the imagepath " + bookfromview.ImagePath);

            }
            bookindb.ImagePath = bookfromview.ImagePath;
            bookindb.bookAuthor = bookfromview.bookAuthor;
            bookindb.bookTitle = bookfromview.bookTitle;
            bookindb.bookDescription = bookfromview.bookDescription;
            bookindb.bookCategory = bookfromview.bookCategory;
            _context.SaveChanges();
            return RedirectToAction("yourspace");
        }
        return RedirectToAction("yourspace");
    }
    [HttpGet("bookdetails")]
    public IActionResult bookdetails()
    {
        return View();
    }
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        //this takes all like including their users
        ViewBag.listlikedbook = _context.Likes.Include(e => e.user).ToList();
        Console.WriteLine("we are at /Dashboard");
        Console.WriteLine("This is The like database with its elements " + ViewBag.listlikedbook.Count);
        //this takes all books including users
        ViewBag.allbooks = _context.Books.Include(e => e.bookCreator).Include(e => e.booklikes).ToList();
        //this is the id of logged user
        ViewBag.id = (int)HttpContext.Session.GetInt32("userId");
        var like = _context.Books.Include(e => e.booklikes).Where(e => e.booklikes.Any(f => f.userId == (int)HttpContext.Session.GetInt32("userId") == true));
        ViewBag.likeBook = _context.Books.Include(e => e.booklikes).Where(e => e.booklikes.Any(f => f.userId == (int)HttpContext.Session.GetInt32("userId") == true));
        //this takes all the comments 
        ViewBag.allcomments = _context.Comments.Include(e => e.user).ToList();
        //this takes all the reviews 
        ViewBag.allreviews = _context.Reviews.ToList();
        ViewBag.likesinbook = _context.Books.Include(e => e.booklikes).ToList();
        Category cat = _context.Categorys.First(e => e.categoryId == 1);
        Console.WriteLine(cat.categoryName);
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("Login")]
    public IActionResult LoginSubmit(LoginUser userSubmission)
    {
        if (ModelState.IsValid)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.email == userSubmission.email);
            if (userInDb == null)
            {
                ModelState.AddModelError("password", "Invalid UserName/Password");
                return View("Login");
            }

            var hasher = new PasswordHasher<LoginUser>();

            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.password, userSubmission.password);

            if (result == 0)
            {
                ModelState.AddModelError("password", "Invalid Password");
                return View("Login");

            }
            HttpContext.Session.SetInt32("userId", userInDb.userId);
            Console.WriteLine("useri" + userInDb.firstName);
            return RedirectToAction("Dashboard");
        }

        return View("Login");
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {


        if (HttpContext.Session.GetInt32("userId") == null)
        {

            return View();
        }

        return RedirectToAction("Dashboard");

    }


    [HttpPost("Register")]
    public IActionResult Register(User user)
    {
        // Check initial ModelState
        if (ModelState.IsValid)
        {
            // If a User exists with provided email
            if (_context.Users.Any(u => u.email == user.email))
            {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                ModelState.AddModelError("UserName", "Email already in use!");

                return View();
                // You may consider returning to the View at this point
            }
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.password = Hasher.HashPassword(user, user.password);
            _context.Users.Add(user);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", user.userId);

            return RedirectToAction("Index");
        }
        return View();
    }


    [HttpGet("addbook")]
    public IActionResult addbook()

    {
        ViewBag.allCategorys = _context.Categorys.ToList();
        return View();
    }

    [HttpPost("addbook")]
    public async Task<IActionResult> addbook(Book model)
    {

        if (ModelState.IsValid)
        {
            if (model.file != null && model.file.Length > 0)
            {
                var fileName = Path.GetFileName(model.file.FileName);
                var fileExtension = Path.GetExtension(fileName);
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "BookImages", uniqueFileName);

                // Save the image file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.file.CopyToAsync(stream);
                }
                model.ImagePath = uniqueFileName;
                Console.WriteLine("This is the imagepath " + model.ImagePath);

            }
            var categoryselected = _context.Categorys.First(e => e.categoryName == model.bookCategory);
            int IDuser = (int)HttpContext.Session.GetInt32("userId");
            model.categoryId = categoryselected.categoryId;
            model.userId = IDuser;
            // Update the model with the saved file path
            Console.WriteLine(model.ImagePath);
            _context.Books.Add(model);
            _context.SaveChanges();

            return RedirectToAction("yourspace");
        }

        foreach (var modelState in ModelState.Values)
        {
            foreach (var error in modelState.Errors)
            {
                Console.WriteLine($"Error: {error.ErrorMessage}");
            }
        }
        Console.WriteLine("The model does not have a valid state ");
        return View(model);
    }

    [HttpGet("yourspace")]
    public IActionResult yourspace()
    {
        int? IDuser = (int)HttpContext.Session.GetInt32("userId");
        var useriLoguar = _context.Users.First(e => e.userId == IDuser);
        ViewBag.User_name = useriLoguar.firstName;
        ViewBag.userbooks = _context.Books.Where(e => e.userId == IDuser).ToList();
        return View();

    }
}










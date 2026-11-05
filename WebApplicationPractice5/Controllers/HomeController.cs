using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplicationPractice5.Models;
using WebApplicationPractice5.ViewModels;

namespace WebApplicationPractice5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       static List<Author> authors = new List<Author>
        {
            new Author { ID = 1, Name = "J.K. Rowling", Country = "United Kingdom" },
            new Author { ID = 2, Name = "George Orwell", Country = "United Kingdom" }
        };
      static  List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Harry Potter", Price = 1500m, AuthorId = 1 },
            new Book { Id = 2, Title = "Fantastic Beasts", Price = 1200m, AuthorId = 1 },
            new Book { Id = 3, Title = "1984", Price = 1000m, AuthorId = 2 }
        };
        private int NextAuthor = 3;
        private int NextBook = 4;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Authors= authors;
            for(int i=0;i<books.Count;i++)
            {
                BookCardViewModel book = new BookCardViewModel();
                book.Id = books[i].Id;
                book.Title = books[i].Title;
                book.Price = books[i].Price;
                book.AuthorName = authors.FirstOrDefault(a => a.ID == books[i].AuthorId)?.Name;
                model.Books.Add(book);

            }
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(string title,decimal price,int authorId)
        {
            if (string.IsNullOrEmpty(title))
                RedirectToAction("Index");
            Book book= new Book();
            book.Title = title;
            book.Price = price;
            book.Id = books.Count + 1;
            book.AuthorId=authorId;
            books.Add(book);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(String name,String country)
        {
            if (string.IsNullOrEmpty(name))
                RedirectToAction("Index");
            Author author= new Author();
            author.Name = name;
            author.Country = country;
            author.ID = authors.Count + 1;
            authors.Add(author);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book book=books.FirstOrDefault(b=>b.Id==id);
            if (book == null)
                RedirectToAction("Index");
            else
                books.Remove(book);
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HelloMVC_Adil.Models;
using HelloMVC_Adil.Util;
using System.Data.Entity;

namespace HelloMVC_Adil.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            //ViewBag.Head = "Hello - 39";

            HttpContext.Response.Cookies["id"].Value = "ca-4353w";
            Session["Name"] = "Adil";

            /*IEnumerable<Book> books = db.Books.ToList();
            ViewBag.Books = books;*/

            var books = db.Books;
            SelectList authors = new SelectList(db.Books, "Author", "Name");
            ViewBag.Authors = authors;
            //ViewBag.Books = books;
            return View(books);
        }

        /*public ActionResult BookIndex()                                       // Question
        {
            var books = db.Books;
            //ViewBag.Books = books;
            return View(books);
        }*/

        /*public ActionResult Index()
        {
            return View("About");
            //return View("~/Views/Some2/SomeView.cshtml");
        }*/

        /*public ViewResult Index()
        {
            return View("~/Views/Some/Index.cshtml");
        }*/

        //---------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public string GetForm(string countires)
        {
            return countires;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult GetList()
        {
            string[] states = new string[] { "Russia", "USA", "Canada", "France"};
            ViewBag.Message = "Это частичное представление.";
            return PartialView(states);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        // асинхронный метод
        public async Task<ActionResult> BookList()
        {
            IEnumerable<Book> books = await db.Books.ToListAsync();
            ViewBag.Books = books;
            return View("Index");
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        public string GetName()
        {
            var val = Session["name"];
            return val.ToString();
        }

        public string GetData()
        {
            string id = HttpContext.Request.Cookies["id"].Value;
            return id.ToString();
        }

        public string GetContext()
        {
            HttpContext.Response.Write("<h1>Hello World</h1>");
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        /*public FileStreamResult GetStream()
        {
            string path = Server.MapPath("~/Files/КН1.pdf");
            // Объект Stream
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/pdf";
            string file_name = "КН1.pdf";
            return File(fs, file_type, file_name);
        }*/

        public FileResult GetBytes()
        {
            string path = Server.MapPath("~/Files/КН1.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/pdf";                    // "application/octet-stream"
            string file_name = "КН1.pdf";
            return File(mas, file_type, file_name);
        }

        public FileResult GetFile()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/КН1.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = "КН1.pdf";
            return File(file_path, file_type, file_name);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult GetVoid(int id)
        {
            if (id > 3)
            {
                //return RedirectToRoute(new { controller = "Home", action = "Contact" });
                //return RedirectToAction("Contact");
                //return RedirectToAction("Square", "Home", new { a = 10, h = 12 });
                //return new HttpStatusCodeResult(404);
                //return HttpNotFound();
                return new HttpUnauthorizedResult();
            }
            return View("About");
        }

        public ActionResult GetImage()
        {
            string path = "../Images/story_stream_pub.PNG";
            return new ImageResult(path);
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир!</h2>");
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult GetBook()
        {
            return View();
        }
        
        [HttpPost]
        public string PostBook()
        {
            string title = Request.Form["title"];
            string author = Request.Form["author"];
            return title +" "+author;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            Purchase purchase = new Purchase { BookId = id, Person = "WhoAmI", Address = "Kazakhstan"};
            return View(purchase);
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }
        private DateTime getToday()
        {
            return DateTime.Now;
        }


        //---------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        //---------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
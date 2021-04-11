using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WAD_Assignment.Models;

namespace WAD_Assignment.Controllers
{
    public class HomeController : Controller
    {
        const string SessionCart = "_Cart";
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
   

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OneFilm()

        {

            Film model = _context.Films.FirstOrDefault();

            return View(model);

        }


        //AllFilms Action returns all films in a list
        public IActionResult AllFilms()

        {

            List<Film> model = _context.Films.ToList();

            return View(model);

        }

        //Film Details Action from accessing the id.
        public IActionResult FilmDetails(int id)

        {

            Film model = _context.Films.Find(id);

            return View(model);

        }

        //Search Action
        public IActionResult Search(String SearchString)

        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var films = from m in _context.Films
                            where m.FilmTitle.Contains(SearchString)
                            select m;

                List<Film> model = films.ToList();

                ViewData["SearchString"] = SearchString;

                return View(model);
            }
            else
            {
                return View();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

        [HttpPost]
        public IActionResult FilmDetails(IFormCollection form)

        {

            int FilmID = int.Parse(form["FilmID"]);
            string FilmTitle = form["FilmTitle"].ToString();
            decimal FilmPrice = Decimal.Parse(form["FilmPrice"]);
            decimal RentPrice = Decimal.Parse(form["RentPrice"]);
            int OrderQuantity = int.Parse(form["OrderQuantity"]);
            CartItem newOrder = new CartItem();
            newOrder.FilmID = FilmID;
            newOrder.FilmTitle = FilmTitle;
            newOrder.FilmPrice = FilmPrice;
            newOrder.RentPrice = RentPrice;
            newOrder.OrderQuantity = OrderQuantity;
            newOrder.OrderDate = DateTime.Now;

            var CartList = new List<CartItem>();
            if (HttpContext.Session.GetString(SessionCart) != null)
            {
                string serialJSON = HttpContext.Session.GetString(SessionCart);
                CartList = JsonSerializer.Deserialize<List<CartItem>>(serialJSON);
                //
                var item = CartList.FirstOrDefault(o => o.FilmID == FilmID);
                if (item != null)
                {
                    item.OrderQuantity += OrderQuantity;
                }
                else
                {
                    CartList.Add(newOrder);
                }

            }
            else
            {
                CartList.Add(newOrder);
            }
            HttpContext.Session.SetString(SessionCart, JsonSerializer.Serialize(CartList));
            return RedirectToAction("FilmDetails");

        }


        [HttpGet]
        public IActionResult ManageCart()

        {
            List<CartItem> cart = new List<CartItem>();
            if (HttpContext.Session.GetString(SessionCart) != null)
            {
                string serialJSON = HttpContext.Session.GetString(SessionCart);
                cart = JsonSerializer.Deserialize<List<CartItem>>(serialJSON);
            }
            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"].ToString();
            }
            return View(cart);

        }

        [HttpPost]
        public IActionResult RemoveCartItem(IFormCollection form)
        {
            int FilmID = int.Parse(form["FilmID"]);
            var CartList = new List<CartItem>();
            if (HttpContext.Session.GetString(SessionCart) != null)
            {
                string serialJSON = HttpContext.Session.GetString(SessionCart);
                CartList = JsonSerializer.Deserialize<List<CartItem>>(serialJSON);
                var item = CartList.FirstOrDefault(o => o.FilmID == FilmID);
                if (item != null)
                {
                    CartList.Remove(item);
                }

            }

            HttpContext.Session.SetString(SessionCart, JsonSerializer.Serialize(CartList));
            TempData["msg"] = "Item Removed";
            return RedirectToAction("ManageCart");
        }

        public IActionResult DeleteSession()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/SessionDemo");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly GameContext _context;
        private readonly IHttpContextAccessor _httpAccessor;

        public CartController(GameContext context, IHttpContextAccessor httpAccessor)
        {
            _context = context;
            _httpAccessor = httpAccessor;
        }

        public async Task<IActionResult> Add(int id)
        {
            // Get the game with the corresponding id
            VideoGame game = await VideoGameDB.GetGameByID(id, _context);

            CartHelper.Add(_httpAccessor, game);

            //// Convert game to a string
            //string data = JsonConvert.SerializeObject(game);
            //// Set up cookie
            //CookieOptions options = new CookieOptions()
            //{
            //    Secure = true,
            //    MaxAge = TimeSpan.FromDays(14)
            //};
            //// Add the game to a cookie
            //_httpAccessor.HttpContext.Response.Cookies.Append("CartCookie", data, options);

            // Redirect to catalog
            return RedirectToAction("Index", "Library");
        }
    }
}
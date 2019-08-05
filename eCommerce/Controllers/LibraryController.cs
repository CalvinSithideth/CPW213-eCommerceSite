using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GameContext _context;

        public LibraryController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VideoGame> allGames = await VideoGameDB.GetAllGames(_context);
            return View(allGames);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(VideoGame game)
        {
            if (ModelState.IsValid)
            {
                // Add to database
                await VideoGameDB.Add(game, _context);
                // Async code "bubbles up".
                // That means this method call must have 'await'
                // and the containing method must be async and return Task<T>
                return RedirectToAction("Index"); // Go back to "library"
            }

            // Return view with model including error messages
            return View(game);
        }

        public async Task<IActionResult> Update(int id)
        {
            VideoGame game = await VideoGameDB.GetGameByID(id, _context);

            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VideoGame game)
        {
            if (ModelState.IsValid)
            {
                await VideoGameDB.UpdateGame(game, _context);
                return RedirectToAction("Index");
            }
            // If there are any errors, show the user
            // the form again
            return View(game);
        }
    }
}
﻿using System;
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
        public async Task<IActionResult> Index(int? id)
        {
            // Null-coalescing operator
            // If id is not null set page to it, or if null use 1
            int page = id ?? 1; // id is the page number coming in
            const int PageSize = 3;
            List<VideoGame> games = await VideoGameDB.GetGamesByPage(_context, page, PageSize);

            int totalPages = await VideoGameDB.GetTotalPages(_context, PageSize);
            ViewData["Pages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(games);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            VideoGame game = await VideoGameDB.GetGameByID(id, _context);

            return View(game);
        }

        [HttpPost, ActionName("Delete")] // This is how we use the same URL with different methods
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await VideoGameDB.DeleteByID(id, _context);
            return RedirectToAction("Index");
        }
    }
}
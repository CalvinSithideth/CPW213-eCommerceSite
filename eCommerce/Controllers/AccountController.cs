﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly GameContext _context;

        public AccountController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Member m)
        {
            if (ModelState.IsValid)
            {
                await MemberDB.Add(_context, m);

                TempData["Message"] = "You registered successfully";
                return RedirectToAction("Index", "Home");

            }

            return View(m);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
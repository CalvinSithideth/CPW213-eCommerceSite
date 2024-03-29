﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Provides access to session data for
        /// the current user
        /// </summary>
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly GameContext _context;

        public AccountController(GameContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _httpAccessor = accessor;
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
                SessionHelper.LogUserIn(_httpAccessor, m.MemberID, m.Username);
                TempData["Message"] = "You registered successfully";
                return RedirectToAction("Index", "Home");

            }

            return View(m);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member =  await MemberDB.IsLoginValid(_context, model);
                if (member != null)
                {
                    TempData["Message"] = "Logged in successfully";
                    SessionHelper.LogUserIn(_httpAccessor, member.MemberID, member.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                { // Credentials invalid
                    // Adding model error with no key, will display error
                    // message in the validation summary
                    ModelState.AddModelError(string.Empty,
                        "I'm sorry, your credentials did not match any record in our database");
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            SessionHelper.LogUserOut(_httpAccessor);
            TempData["Message"] = "You have been logged out";
            return RedirectToAction("Index", "Home");
        }
    }
}
using System;
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

        //Add constructor with DB context as a parameter
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
        public async Task<IActionResult> Register(Member m) 
        {
            if (ModelState.IsValid) 
            {
                //Add member to database
                await MemberDb.Add(_context, m);

                //Display success message on home page after redirecting
                TempData["Message"] = "You registered successfully";

                return RedirectToAction("Index", "Home");
            }
            return View(m);
        }
    }
}
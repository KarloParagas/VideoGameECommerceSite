using System;
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
        /// Provides access to session data for the current user
        /// </summary>
        private readonly IHttpContextAccessor _httpAccessor;

        private readonly GameContext _context;

        //Add constructor with DB context as a parameter
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

        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                Member member = await MemberDb.IsLoginValid(model, _context);
                if (member != null)
                {
                    TempData["Message"] = "Logged in successfully";

                    //Create session for user (Set session data)
                    _httpAccessor.HttpContext.Session.SetInt32("MemberId", member.MemberId);
                    _httpAccessor.HttpContext.Session.SetString("Username", member.Username);

                    return RedirectToAction("Index", "Home");
                }
                else //Credentials invalid
                {
                    //Adding model error with no key, will display error message in the validation summary
                    ModelState.AddModelError(string.Empty, "Your credentials did not match any record in our database");    
                }
            }
            return View(model);
        }

        public IActionResult Logout() 
        {
            _httpAccessor.HttpContext.Session.Clear();
            TempData["Message"] = "You have been logged out";
            return RedirectToAction("Index", "Home");
        }
    }
}
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
        private readonly GameContext _context; //readonly makes it so that the constructor is the only one that can modify it

        public LibraryController(GameContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(VideoGame game) 
        {
            if (ModelState.IsValid)
            {
                //Add to database
                _context.Add(game);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            //Return view with model including error messages
            return View(game);
        }
    }
}
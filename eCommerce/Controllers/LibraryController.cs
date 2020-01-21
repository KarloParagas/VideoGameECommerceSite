using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class LibraryController : Controller
    {
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
                return RedirectToAction("Index");
            }
            //Return view with model including error messages
            return View(game);
        }
    }
}
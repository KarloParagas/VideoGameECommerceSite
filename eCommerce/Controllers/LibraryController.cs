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

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            //Null-coalescing operator
            //id is the page number coming in
            int page = id ?? 1; //if the id is not null, it sets page to it. If it's null, use 1.
            List<VideoGame> games = await VideoGameDb.GetGamesByPage(_context, page, 3);
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
                //Add to database
                await VideoGameDb.AddAsync(game, _context);

                return RedirectToAction("Index");
            }
            //Return view with model including error messages
            return View(game);
        }

        public async Task<IActionResult> Update(int id) 
        {
            VideoGame game = await VideoGameDb.GetGameById(id, _context);
            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VideoGame g) 
        {
            if (ModelState.IsValid) 
            {
                await VideoGameDb.UpdateGame(g, _context);
                return RedirectToAction("Index");
            }

            //If there are any errors, show the user the form again
            return View(g);
        }

        public async Task<IActionResult> Delete(int id) 
        {
            VideoGame game = await VideoGameDb.GetGameById(id, _context);
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            await VideoGameDb.DeleteById(id, _context);
            return RedirectToAction("Index");
        }
    }
}
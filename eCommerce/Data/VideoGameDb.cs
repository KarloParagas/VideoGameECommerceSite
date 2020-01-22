using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    /// <summary>
    /// DB Helper class for VideoGames
    /// </summary>
    public static class VideoGameDb
    {
        /// <summary>
        /// Adds a VideoGame to the data store and sets the ID value
        /// </summary>
        /// <param name="g">The game to add</param>
        /// <param name="context">The DB context to use</param>
        public static async Task<VideoGame> AddAsync(VideoGame g, GameContext context) 
        {
            await context.AddAsync(g);
            await context.SaveChangesAsync();
            return g;
        }

        /// <summary>
        /// Returns the total number of pages needed to have <paramref name="pageSize"/> amount of products per page
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<int> GetTotalPages(GameContext context, int pageSize)
        {
            int totalNumGames = await context.VideoGames.CountAsync();

            //Partial number of pages
            double pages = (double)totalNumGames / pageSize;
            return (int)Math.Ceiling(pages);
        }

        //https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.0#asynchronous-code

        /// <summary>
        /// Retrieves all games sorted in alphabetical order by title
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> GetAllGames(GameContext context)
        {
            //LINQ Query syntax
            //List<VideoGame> games = await (from vidGame in context.VideoGames
            //                               orderby vidGame.Title ascending
            //                               select vidGame).ToListAsync();

            //LINQ Method Syntax
            List<VideoGame> games = await context.VideoGames
                                                 .OrderBy(g => g.Title)
                                                 .ToListAsync();

            return games;
        }

        public static async Task<VideoGame> UpdateGame(VideoGame g, GameContext context)
        {
            context.Update(g);
            await context.SaveChangesAsync();
            return g;
        }

        public static async Task DeleteById(int id, GameContext context)
        {
            //Create video game object, with the id of the game we want to remove from the database
            VideoGame g = new VideoGame()
            {
                Id = id
            };
            context.Entry(g).State = EntityState.Deleted; //This tells the Entity Framework that we have the video game object, but we are removing it from the database
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a game with a specified ID. If no game is found, null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<VideoGame> GetGameById(int id, GameContext context)
        {
            VideoGame g = await (from game in context.VideoGames
                                 where game.Id == id
                                 select game).SingleOrDefaultAsync();

            return g;
        }

        /// <summary>
        /// Returns 1 page worth of products. Products are sorted alphabetically by Title.
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="pageNum">The page number for the products</param>
        /// <param name="pageSize">The number of products per page</param>
        public static async Task<List<VideoGame>> GetGamesByPage(GameContext context, int pageNum, int pageSize) 
        {

            //Make sure to call skip BEFORE take
            //Make sure orderby comes first
            List<VideoGame> games = await context.VideoGames
                                                 .OrderBy(vg => vg.Title) //Orders by title
                                                 .Skip((pageNum - 1) * pageSize) //Skips the pages needed to get to the desired page
                                                 .Take(pageSize) //Takes the products desired
                                                 .ToListAsync(); //Converts it to a list                                                 

            return games;
        }
    }
}

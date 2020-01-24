using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Contains helper methods to manage the users shopping cart
    /// </summary>
    public static class CartHelper
    {
        private const string CART_COOKIE = "Cart";

        /// <summary>
        /// Gets the current users VideoGames from their shopping cart.
        /// If there are no games, an empty list is returned.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static List<VideoGame> GetGames(IHttpContextAccessor accessor) 
        {
            //Get data out of cookie
            string data = accessor.HttpContext.Request.Cookies[CART_COOKIE];

            if (string.IsNullOrWhiteSpace(data)) //If there's no cookie data
            {
                return new List<VideoGame>(); //Return an empty list
            }

            List<VideoGame> games = JsonConvert.DeserializeObject<List<VideoGame>>(data);

            return games;
        }

        /// <summary>
        /// Get total number of video games in the cart
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static int GetGameCount(IHttpContextAccessor accessor) 
        {
            List<VideoGame> allGames = GetGames(accessor);
            return allGames.Count;
        }

        /// <summary>
        /// Adds VideoGame to the cart
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="g">Video game to be added</param>
        public static void Add(IHttpContextAccessor accessor, VideoGame g) 
        {
            //Get all the games that user currently has
            List<VideoGame> games = GetGames(accessor);

            //Add a new one to the list
            games.Add(g);
            //games.OrderBy(game => game.Title); //Sorts by title if needed

            string data = JsonConvert.SerializeObject(games); //Turns the any object to a string

            //Append it to their cookies
            accessor.HttpContext.Response.Cookies.Append(CART_COOKIE, data);
        }
    }
}

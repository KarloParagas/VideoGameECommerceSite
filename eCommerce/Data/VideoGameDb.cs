using eCommerce.Models;
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
        public static VideoGame Add(VideoGame g, GameContext context) 
        {
            context.Add(g);
            context.SaveChanges();
            return g;
        } 
    }
}

using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    /// <summary>
    /// The database context class for the video game store
    /// </summary>
    public class GameContext : DbContext //":" is Java's equivalent of "inherits"
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) //base(parameter) is similar to Java's "super" keyword 
        {
            //Don't need anything in this constructor, because we're passing in our DbContextOptions object to the base class, and the base class
            //handles all the functionality
        }

        //Add a DbSet<T> for each entity you want to keep track of in the database
        public DbSet<VideoGame> VideoGames { get; set; }
    }
}

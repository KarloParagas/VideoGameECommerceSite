using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents a single video game
    /// </summary>
    public class VideoGame
    {
        /// <summary>
        /// Unique ID number for the game
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The game title
        /// </summary>
        [Required]
        [StringLength(90)]
        public string Title { get; set; }

        /// <summary>
        /// Official ESRB rating
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// The game description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Retail price for the game
        /// </summary>
        [Range(0.01, 999.99)]
        [DataType(DataType.Currency)]
        public double Price { get; set; } //Required by default because double is a value type
    }
}

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
    public static class VideoGameDB
    {
        /// <summary>
        /// Adds a VideoGame to the data store and sets
        /// the ID value
        /// </summary>
        /// <param name="game">The game to be added</param>
        /// <param name="context">The DB context to use</param>
        public static VideoGame Add(VideoGame game, GameContext context)
        {
            context.Add(game);
            context.SaveChanges();
            return game;
        }
    }
}

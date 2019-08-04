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
        public static async Task<VideoGame> Add(VideoGame game, GameContext context)
        {
            /* Async code
             * Instead of Add and SaveChanges, we use Async versions.
             * Async methods use await keyword
             * await keywork must be used in an async method
             * async methods can only return void and Task<T>
             * Task<VideoGame> will still return a VideoGame
             */
            await context.AddAsync(game);
            await context.SaveChangesAsync();
            return game;
        }
    }
}

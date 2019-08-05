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

        /// <summary>
        /// Retrieves all games sorted in alphabetical order
        /// by title
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> GetAllGames(GameContext context)
        {
            // LINQ Query syntax
            /*
            List<VideoGame> games =
                await (from row in context.VideoGames
                orderby row.Title ascending
                select row).ToListAsync();
            return games;
            */

            // LINQ Method Syntax
            List<VideoGame> games = await context.VideoGames
                .OrderBy(g => g.Title)
                .ToListAsync();
            return games;
        }

        public static async Task<VideoGame> UpdateGame(VideoGame game, GameContext context)
        {
            context.Update(game);
            await context.SaveChangesAsync();
            return game;
        }

        /// <summary>
        /// Gets a game with a specified ID. If no game is found,
        /// null is returned
        /// </summary>
        /// <param name="id">The ID of the game you want to return</param>
        /// <param name="context">The DB context to use</param>
        /// <returns></returns>
        public static async Task<VideoGame> GetGameByID(int id, GameContext context)
        {
            VideoGame game = await (from row in context.VideoGames
                              where row.ID == id
                              select row).SingleOrDefaultAsync();
            return game;
        }
    }
}

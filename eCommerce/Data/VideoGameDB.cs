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
        /// Returns 1 page worth of products. Products are
        /// sorted alphabetically by Title
        /// </summary>
        /// <param name="context">The DB context</param>
        /// <param name="pageNum">The page number for the products</param>
        /// <param name="pageSize">The number of products per page</param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> GetGamesByPage(GameContext context, int pageNum, int pageSize)
        { // See SQL 'Offset' and 'Fetch'
            List<VideoGame> games = await
                context.VideoGames
                // OrderBy must be called first, else it will sort the result at the end
                .OrderBy(vg => vg.Title) // Order by title
                .Skip((pageNum - 1) * pageSize) // SQL Offset - Must be called before Take()
                .Take(pageSize) // SQL Fetch
                .ToListAsync();
            return games;
        }

        /// <summary>
        /// Returns the total number of pages needed
        /// to have <paramref name="pageSize"/> amount of products per page
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<int> GetTotalPages(GameContext context, int pageSize)
        {
            int totalNumGames = await context.VideoGames.CountAsync();
            double pages = (double) totalNumGames / pageSize; // Might miss a page with integer division
            return (int) Math.Ceiling(pages);
        }

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

        public static async Task DeleteByID(int id, GameContext context)
        {
            // Create VideoGame object, with the ID of the game
            // we want to remove from the database
            VideoGame game = new VideoGame()
            {
                ID = id
            };
            context.Entry(game).State = EntityState.Deleted;
            await context.SaveChangesAsync();
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

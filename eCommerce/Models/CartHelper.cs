using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Contains helper methods to manage the user's shopping cart
    /// </summary>
    public class CartHelper
    {
        private const string CartCookie = "Cart";

        /// <summary>
        /// Gets the current user's VideoGames from their shopping cart.
        /// If there are no games, an empty list is returned
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static List<VideoGame> GetGames(IHttpContextAccessor accessor)
        {
            // Get data out of cookie
            string data = accessor.HttpContext.Request.Cookies[CartCookie];

            if (string.IsNullOrWhiteSpace(data))
            {
                return new List<VideoGame>();
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
        /// Adds VideoGame to the existing cart data.
        /// If no cart cookie exists, it will be created
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="game">Video game to be added</param>
        public static void Add(IHttpContextAccessor accessor, VideoGame game)
        {
            List<VideoGame> games = GetGames(accessor);
            games.Add(game);

            string data = JsonConvert.SerializeObject(games);

            accessor.HttpContext.Response.Cookies.Append(CartCookie, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Models
{
    /// <summary>
    /// Helper class to provide Session
    /// management
    /// </summary>
    public static class SessionHelper
    {
        private const string MemberIdKey = "MemberID";
        private const string UsernameKey = "Username";

        public static void LogUserIn(IHttpContextAccessor context, int memberId, string username)
        {
            context.HttpContext.Session.SetInt32(MemberIdKey, memberId);
            context.HttpContext.Session.SetString(UsernameKey, username);
        }

        public static Boolean IsUserLoggedIn(IHttpContextAccessor context)
        {
            if (context.HttpContext.Session.GetInt32(MemberIdKey).HasValue)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Destroys the current user's session
        /// </summary>
        /// <param name="context"></param>
        public static void LogUserOut(IHttpContextAccessor context)
        {
            context.HttpContext.Session.Clear();
        }

        /// <summary>
        /// Gets the username of the current user if
        /// they are logged in. Null is returned if the user
        /// is not logged in
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUsername(IHttpContextAccessor context)
        {
            return context.HttpContext.Session.GetString(UsernameKey);
        }

        /// <summary>
        /// Returns the MemberID of the currently logged in user.
        /// MemberID will be null if they are not logged in.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int? GetMemberID(IHttpContextAccessor context)
        {
            return context.HttpContext.Session.GetInt32(MemberIdKey);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Helper class to provide session management
    /// </summary>
    public static class SessionHelper
    {
        private const string MEMBER_ID_KEY = "MemberId";
        private const string USERNAME_KEY = "Username";

        public static void LogUserIn(IHttpContextAccessor context, int memberId, string username) 
        {
            context.HttpContext.Session.SetInt32(MEMBER_ID_KEY, memberId);
            context.HttpContext.Session.SetString(USERNAME_KEY, username);
        }

        public static bool IsUserLoggedIn(IHttpContextAccessor context) 
        {
            if (context.HttpContext.Session.GetInt32(MEMBER_ID_KEY).HasValue) //If they have a MemberId stored in session
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Destroys the current users session
        /// </summary>
        /// <param name="context"></param>
        public static void LogUserOut(IHttpContextAccessor context) 
        {
            context.HttpContext.Session.Clear();
        }

        /// <summary>
        /// Gets the username of the current user if they are logged in. Null is returned if the user is not logged in.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUsername(IHttpContextAccessor context) 
        {
            return context.HttpContext.Session.GetString(USERNAME_KEY);
        }

        /// <summary>
        /// Returns the MemberId of the currently logged in user. 
        /// MemberId will be null if they are not logged in.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int? GetMemberId(IHttpContextAccessor context) 
        {
            return context.HttpContext.Session.GetInt32(MEMBER_ID_KEY);
        }
    }
}

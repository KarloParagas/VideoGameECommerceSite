using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    public static class MemberDb
    {
        /// <summary>
        /// Adds a member to the database. Returns the member with their MemberId populated.
        /// </summary>
        /// <param name="context">The database context to be used</param>
        /// <param name="m">The new Member to be added</param>
        /// <returns></returns>
        public async static Task<Member> Add(GameContext context, Member m) 
        {
            context.Members.Add(m);
            await context.SaveChangesAsync();
            return m;
        }

        /// <summary>
        /// Checks if credentials are found in the database. The matching member is returned for valid credentials. 
        /// Null is returned if there are no matches
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        public async static Task<Member> IsLoginValid(LoginViewModel model, GameContext context)
        {
            return await (from m in context.Members
                             where m.Username == model.UsernameOrEmail 
                                || m.EmailAddress == model.UsernameOrEmail
                                && m.Password == model.Password
                             select m).SingleOrDefaultAsync();
        }
    }
}

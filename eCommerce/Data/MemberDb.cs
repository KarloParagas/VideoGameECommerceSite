using eCommerce.Models;
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
    }
}

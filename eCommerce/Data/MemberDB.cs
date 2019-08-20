using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    public static class MemberDB
    {
        /// <summary>
        /// Adds a member to the database. Returns the member
        /// with their MemberID populated
        /// </summary>
        /// <param name="context"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public async static Task<Member> Add(GameContext context, Member m)
        {
            context.Members.Add(m);
            await context.SaveChangesAsync();
            return m;
        }
    }
}

using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Checks if credentials are found in the daatabase.
        /// The matching member is returned for valid
        /// credentials. Null is returned if there are
        /// no matches
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async static Task<Member> IsLoginValid(GameContext context, LoginViewModel model)
        {
            return await
                (from m in context.Members
                 where (m.Username == model.UsernameOrEmail
                     || m.EmailAddress == model.UsernameOrEmail)
                     && m.Password == model.Password
                 select m).SingleOrDefaultAsync();
        }
    }
}

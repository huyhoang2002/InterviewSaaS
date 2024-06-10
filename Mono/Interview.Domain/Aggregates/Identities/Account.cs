using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Identities
{
    public class Account : IdentityUser
    {
        private List<Token> tokens => new List<Token>();
        public IReadOnlyCollection<Token> Tokens => tokens;

        public Account()
        {

        }

        public Account(string email, string username)
        {
            Email = email;
            UserName = username;
        }

        public void StoreToken(string accessToken, string refreshToken, bool blagFlag, string accountId)
        {
            tokens.Add(new Token(accessToken, refreshToken, blagFlag, accountId));
        }
    }
}

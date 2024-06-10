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
        private List<Token> tokens { get; set; } = new List<Token>();
        public virtual IReadOnlyCollection<Token> Tokens => tokens;

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
            var token = new Token(accessToken, refreshToken, blagFlag, accountId);
            tokens.Add(token);
        }

        public void FindAndModifyActiveToken(string accountId)
        {
            var tokensWithActiveBlagFlag = tokens.Where(_ => _.AccountId == accountId);
            foreach(var token in tokensWithActiveBlagFlag)
            {
                if (token.BlagFlag == false)
                {
                    token.ExposeBlagFlag();
                }
            }
        }
    }
}

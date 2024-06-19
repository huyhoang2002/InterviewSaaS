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

        public Token GetActiveToken(string accountId)
        {
            var token = Tokens.FirstOrDefault(_ => _.AccountId == accountId && _.BlagFlag == false);
            return token;
        }

        public bool CompareRefreshToken(Token token, string refreshToken)
        {
            return token.RefreshToken == refreshToken;
        }
        
        public void RefreshToken(string accountId, string accessToken, string refreshToken)
        {
            var activeToken = GetActiveToken(accountId);
            activeToken.AccessToken = accessToken;
            activeToken.RefreshToken = refreshToken;
        }
    }
}

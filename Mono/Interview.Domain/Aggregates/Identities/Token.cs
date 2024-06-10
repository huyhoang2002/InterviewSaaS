using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Identities
{
    public class Token : EntityBase<Guid>
    {
        public Token()
        {

        }
        public Token(string accessToken, string refreshToken, bool blagFlag, string accountId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            BlagFlag = blagFlag;
            AccountId = accountId;
            CreatedAt = DateTime.Now;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool BlagFlag { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }

        public void ExposeBlagFlag()
        {
            BlagFlag = true;
        }
    }
}

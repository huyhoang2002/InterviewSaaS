using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Identities
{
    public class Token
    {
        public string TokenId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool BlagFlag { get; set; }
        public string AccountId { get; set; }
    }
}

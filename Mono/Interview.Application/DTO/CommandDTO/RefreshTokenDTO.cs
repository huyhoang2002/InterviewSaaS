using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.CommandDTO
{
    public record RefreshTokenDTO(string accessToken, string RefreshToken);
}

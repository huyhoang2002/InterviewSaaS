using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.ResponseDTO
{
    public record TokenResponseDTO(
            string AccessToken,
            string RefreshToken,
            string TokenType
        );
   
}

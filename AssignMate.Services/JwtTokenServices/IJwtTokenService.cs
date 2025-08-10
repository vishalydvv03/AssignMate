using AssignMate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Services.JwtServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}

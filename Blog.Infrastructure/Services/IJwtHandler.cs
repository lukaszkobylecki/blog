using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(int userId);
    }
}

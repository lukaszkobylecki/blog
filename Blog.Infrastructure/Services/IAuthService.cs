using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public interface IAuthService : IService
    {
        Task LoginAsync(string email, string password);
    }
}

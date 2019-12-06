using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public interface IAccountService : IService
    {
        Task ChangePassword(Guid userId, string currentPassword, string newPassword);
    }
}

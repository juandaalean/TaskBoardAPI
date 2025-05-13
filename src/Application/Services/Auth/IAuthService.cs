using Application.Dtos.User;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth
{
    public interface IAuthService
    {
        Task<ErrorOr<string>> RegisterAsync(UserRegisterDto request);
        Task<ErrorOr<string>> LoginAsync(UserLoginDto request);
    }
}

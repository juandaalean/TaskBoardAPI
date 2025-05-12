using Application.Dtos.User;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using ErrorOr;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenGenerator _tokenGenerator;
        public AuthService(IUserRepository userRepo, IJwtTokenGenerator tokenGenerator)
        {
            _userRepo = userRepo;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ErrorOr<string>> LoginAsync(UserLoginDto request)
        {
            var user = await _userRepo.GetUserByUsernameAsync(request.Username);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Error.Unauthorized(description: "Invalid Credentials");
            }
            return _tokenGenerator.GenerateToken(user);
        }

        public async Task<ErrorOr<string>> RegisterAsync(UserRegisterDto request)
        {
            if (await _userRepo.UserExistAsync(request.Username))
            {
                return Error.Conflict(description: "User already exist.");
            }

            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Roles.User
            };

            await _userRepo.AddUserAsync(user);
            return "Register Successfully!";
        }
    }
}

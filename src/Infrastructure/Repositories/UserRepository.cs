using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly TaskBoardContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(TaskBoardContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _unitOfWork.SaveChangerAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> UserExistAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}

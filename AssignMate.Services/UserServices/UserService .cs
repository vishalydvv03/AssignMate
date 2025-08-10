using AssignMate.Core.Entities;
using AssignMate.Core.Enums;
using AssignMate.Core.Models;
using AssignMate.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly IPasswordHasher<User> hasher;
        public UserService(AppDbContext context, IPasswordHasher<User> hasher)
        {
            this.context = context;
            this.hasher = hasher;
        }

        public async Task<bool> RegisterUser(UserRegister dto)
        {
            var userExists = await context.Users.AnyAsync(u=>u.Email==dto.Email);
            if (userExists)
            {
                return false;
            }
            var newUser = new User()
            {
                Email = dto.Email,
                PasswordHash = hasher.HashPassword(null, dto.PasswordHash),
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Role = dto.Role,
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LoginUser(UserLogin dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u=>u.Email==dto.Email);
            if(user!=null)
            {
                var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
                if(result == PasswordVerificationResult.Success)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<User>> GetAllStudentsList()
        {
            var users = await context.Users.Where(u=>u.Role==UserRole.Student).ToListAsync();
            return users;
        }
    }
}

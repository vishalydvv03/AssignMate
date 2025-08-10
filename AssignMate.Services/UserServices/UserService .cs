using AssignMate.Core.Entities;
using AssignMate.Core.Enums;
using AssignMate.Core.Models;
using AssignMate.Services.JwtServices;
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
        private readonly IJwtTokenService tokenService;
        public UserService(AppDbContext context, IPasswordHasher<User> hasher, IJwtTokenService tokenService)
        {
            this.context = context;
            this.hasher = hasher;
            this.tokenService = tokenService;
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

        public async Task<string?> LoginUser(UserLogin dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u=>u.Email==dto.Email);
            if(user!=null)
            {
                var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
                if(result == PasswordVerificationResult.Success)
                {
                    var token = tokenService.GenerateToken(user);
                    return token;
                }
            }
            return null;
        }

        public async Task<List<User>> GetAllStudentsList()
        {
            var users = await context.Users.Where(u=>u.Role==UserRole.Student).ToListAsync();
            return users;
        }
        public async Task<ReadUser?> GetUserById(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                var userModel = new ReadUser()
                {
                    Id = id,
                    Name = user.Name,
                    Role = user.Role,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                };
                return userModel;
            }
            return null;
        }
       
        public async Task<bool> UpdateUser(int id, ReadUser dto)
        {
            var user = await context.Users.FindAsync(id);
            if(user != null)
            {
                user.Email = dto.Email;
                user.Name = dto.Name;
                user.Role = dto.Role;
                user.DateOfBirth = dto.DateOfBirth;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

using AssignMate.Core.Entities;
using AssignMate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRegister dto);
        Task<string?> LoginUser(UserLogin dto);
        Task<List<User>> GetAllStudentsList();
        Task<ReadUser?> GetUserById(int id);
        Task<bool> UpdateUser(int id, ReadUser dto);
        Task<bool> DeleteUser(int id);
    }
}

using ClothingStore.Entities.Dtos;
using ClothingStore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> register(RegisterDto user);
        public Task<bool> isExist(string username);
        public Task<bool> Login(LoginDto user);
        public Task<User> GetUserByUsername(string username);
        public Task<bool> ChangePassword(ChangePasswordDto value);
        public Task<IEnumerable<UserProfileDto>> GetUsers();
        public Task<IEnumerable<Role>> GetRoles();
        public Task<bool> ChangeRole(ChangeRoleDto value);

        public Task<bool> AddRefreshToken(RefreshToken refreshToken);
        public Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        public Task<bool> ValidateRefreshToken(string token, string ip);
    }
}

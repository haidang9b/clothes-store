using ClothingStore.Entities;
using ClothingStore.Entities.Dtos;
using ClothingStore.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClothingContext _dbContext;
        public UserRepository(ClothingContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto value)
        {
            var old = await _dbContext.users.FirstOrDefaultAsync(x => x.Username == value.Username);

            old.Password = BCrypt.Net.BCrypt.HashPassword(value.PasswordNew, BCrypt.Net.SaltRevision.Revision2Y);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _dbContext.users.Include(u => u.Role).Where(u => u.Username == username).FirstOrDefaultAsync();
            if (user is null) return null;
            return user;
        }

        public async Task<IEnumerable<UserProfileDto>> GetUsers()
        {
            var result = await _dbContext.users.Select(x =>
            new UserProfileDto
            {
                Id = x.Id,
                FullName = x.FullName,
                RoleId = x.RoleId,
                Username = x.Username
            }).ToListAsync();
            return result;
        }

        public async Task<bool> isExist(string username)
        {
            var result = await _dbContext.users.Where(u => u.Username == username).ToListAsync();
            return result.Count > 0;
        }

        public Task<bool> Login(LoginDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> register(RegisterDto user)
        {
            var newUser = new User { Username = user.Username.ToLower(), FullName = user.FullName, RoleId = 3 };
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.SaltRevision.Revision2Y);
            _dbContext.users.Add(newUser);
            var rs = await _dbContext.SaveChangesAsync();
            return rs > 0;
        }
        public async Task<Role> UserHasRole(string username)
        {
            var user = await _dbContext.users.Include(p => p.Role).FirstOrDefaultAsync(u => u.Username == username);
            return user.Role;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _dbContext.roles.ToListAsync();
        }

        public async Task<bool> ChangeRole(ChangeRoleDto value)
        {
            var old = await _dbContext.users.FirstOrDefaultAsync(x => x.Id == value.UserId);
            if (old is null) return false;
            old.RoleId = value.RoleId;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }

}

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
            var old = await _dbContext.users.FirstOrDefaultAsync(x => x.Username == value.username);

            old.Password = BCrypt.Net.BCrypt.HashPassword(value.passwordNew, BCrypt.Net.SaltRevision.Revision2Y);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _dbContext.users.Include(u => u.role).Where(u => u.Username == username).FirstOrDefaultAsync();
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
                role_id = x.role_id,
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
            var newUser = new User { Username = user.Username.ToLower(), FullName = user.FullName, role_id = 3 };
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.SaltRevision.Revision2Y);
            _dbContext.users.Add(newUser);
            var rs = await _dbContext.SaveChangesAsync();
            return rs > 0;
        }
        public async Task<Role> UserHasRole(string username)
        {
            var user = await _dbContext.users.Include(p => p.role).FirstOrDefaultAsync(u => u.Username == username);
            return user.role;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _dbContext.roles.ToListAsync();
        }

        public async Task<bool> ChangeRole(ChangeRoleDto value)
        {
            var old = await _dbContext.users.FirstOrDefaultAsync(x => x.Id == value.user_id);
            if (old is null) return false;
            old.role_id = value.role_id;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddRefreshToken(RefreshToken refreshToken)
        {
            await _dbContext.refreshTokens.AddAsync(refreshToken);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken model)
        {
            var old = await _dbContext.refreshTokens.FirstOrDefaultAsync(r => r.refreshToken == model.refreshToken && r.CreatedByIp == model.CreatedByIp && r.CreatedDate.AddDays(5) >= DateTime.Now);
            _dbContext.refreshTokens.Remove(old);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ValidateRefreshToken(string token, string ip)
        {
            var tokens = await _dbContext.refreshTokens.Where(r => r.refreshToken.Contains(token) && r.CreatedByIp.Contains(ip) && r.CreatedDate.AddDays(5) >= DateTime.Now).ToListAsync();
            if (tokens.Count > 0)
                return true;
            return false;
        }
    }

}

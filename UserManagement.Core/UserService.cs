using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using UsersManagement.Domain.Models;
using UsersManagement.Domain.Roles;
using UsersManagement.Domain.Users;
using UsersManagement.Infrastructure;

namespace UsersManagement.Core
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> AddUserAsync(UserModelCreate userModelCreate)
        {
            var user = new User
            {
                Name = userModelCreate.Name,
                Email = userModelCreate.Email,
                RoleId = userModelCreate.RoleId,
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();       // на этом этапе в базе SQL создается ID. И теперь мы можем передать его в модель и прокинуть дальше. 

            var userModel = await GetUserAsync(user.Id);

            return userModel;
        }

        public async Task<UserModel> GetUserAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null) { return null; }

            var userModel = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.Name

            };

            return userModel;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = await _dbContext.Users.Include(x => x.Role ).ToListAsync();

            var usersModel = new List<UserModel>();

            foreach (var user in users)
            {
                var model = new UserModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name
                };

                usersModel.Add(model);
            }

            return usersModel;
        }

        public async Task<IEnumerable<UserModel>> GetUsersPageAsync(int skip, int take, string searchText)
        {
            var usersdb = _dbContext.Users.AsQueryable();

            if (!String.IsNullOrEmpty(searchText))
            {
                usersdb = usersdb.Where(x => x.Name.Contains(searchText) || x.Email.Contains(searchText));
            }

            var users = await usersdb.Skip(skip).Take(take).ToListAsync();

            var usersModel = new List<UserModel>();

            foreach (var user in users)
            {
                var model = new UserModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    RoleId = user.RoleId
                };

                usersModel.Add(model);
            }

            return usersModel;
        }

        public async Task<UserModel> UpdateUserAsync(UserModel userModel)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userModel.Id);

            if (user == null)
            {
                return null;
            }

            user.Name = userModel.Name;
            user.Email = userModel.Email;
            user.RoleId = userModel.RoleId;

            //_dbContext.Users.Update(user); // для asnotracking если бы он был.
            await _dbContext.SaveChangesAsync();

            return userModel;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            var result = await _dbContext.SaveChangesAsync();

            return result != -1;
        }
    }
}


















//var users = await _dbContext.Users.Where(x => x.Name.Contains(searchText) || x.Email.Contains(searchText)).Skip(skip).Take(take).ToListAsync();
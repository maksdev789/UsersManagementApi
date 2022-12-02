using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Models;

namespace UsersManagement.Core
{
    public interface IUserService
    {
        /// <summary>
        /// ВОзвращает модель заполненную пользователя
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<UserModel> AddUserAsync(UserModelCreate userModelCreate);

        Task<UserModel> GetUserAsync(int userId);

        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        Task<IEnumerable<UserModel>> GetUsersPageAsync(int skip, int take, string searchText);

        Task<UserModel> UpdateUserAsync(UserModel userModel);

        Task<bool> DeleteUserAsync(int userId);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Models
{   
    /// <summary>
    /// В эту модель мы принимаем заполненные пользователем поля с фронта
    /// </summary>
    public class UserModelCreate
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

    }
}

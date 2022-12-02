using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Models
{
    public class UserModel
    {
        /// <summary>
        /// Id создается в момент создания пользователя в базе 
        /// </summary>
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}

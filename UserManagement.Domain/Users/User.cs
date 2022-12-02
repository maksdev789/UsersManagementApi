using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Roles;

namespace UsersManagement.Domain.Users
{
    public class User
    {   
        /// <summary>
        /// Id создается в момент создания пользователя в базе 
        /// </summary>
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}

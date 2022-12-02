using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Roles;
using UsersManagement.Domain.Users;

namespace UsersManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Метод описывает все ограничения и настройи уровня таблиц SQL  
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
        }
    }
}


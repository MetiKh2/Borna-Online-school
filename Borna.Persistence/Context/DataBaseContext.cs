using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Borna.Domain.Entities.Order;
using Borna.Domain.Entities.Permission;
using Borna.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Persistence.Context
{
    public class DataBaseContext : DbContext,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        #endregion

        #region Permiision
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        #region Course
        public DbSet<CourseGroupe> CourseGroupes { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseLanguage> CourseLanguages { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }

        #endregion
        #region Order
        public DbSet<Order> Orders { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                  .SelectMany(t => t.GetForeignKeys())
                  .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<CourseGroupe>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<Course>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<CourseEpisode>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<CourseComment>().HasQueryFilter(p=>!p.IsRemoved);


            base.OnModelCreating(modelBuilder);
        }

    }
}

using Borna.Domain.Entities.Course;
using Borna.Domain.Entities.Order;
using Borna.Domain.Entities.Permission;
using Borna.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Borna.Application.Interfaces
{
    public interface IDataBaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Permission> Permission { get; set; }
        DbSet<RolePermission> RolePermission { get; set; }
        DbSet<CourseGroupe> CourseGroupes { get; set; }
        DbSet<CourseEpisode> CourseEpisodes { get; set; }
        DbSet<CourseLanguage> CourseLanguages { get; set; }
        DbSet<CourseLevel> CourseLevels { get; set; }
        DbSet<CourseStatus> CourseStatuses { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Order> Orders { get; set; }
       DbSet<UserCourse> UserCourses { get; set; }
       DbSet<CourseComment> CourseComments { get; set; }
        DbSet<CourseVote> CourseVotes { get; set; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}

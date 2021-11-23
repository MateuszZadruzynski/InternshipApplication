using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Company> companies { get; set; }
        public DbSet<Diary> diaries { get; set; }
        public DbSet<Keeper> keepers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Internship> internships { get; set; }
        public DbSet<CompanyImage> images { get; set; }
        public DbSet<StudentAvatars> studentAvatars { get; set; }
        public DbSet<KeeperAvatars> keeperAvatars { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<StudentKeeper> studentKeeper { get; set; }
        public DbSet<QuestionnaireModel> QuestionnaireModels { get; set; }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Comments;
using VacaYAY.Entities.Contracts;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;
using VacaYAY.Entities.ExtraDaysAditions;
using VacaYAY.Entities;

namespace VacaYAY.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static ApplicationDbContext context;

        public static ApplicationDbContext Context
        {
            get
            {
                if (context==null)
                {
                    context = new ApplicationDbContext();
                }
                return context;
            }
        }
        public ApplicationDbContext()
            : base("VacaYaYCodeFirstDB", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            Database.CreateIfNotExists();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }
        public DbSet<ExtraDaysAdition> ExtraDaysAditions { get; set; }
        
    }
}

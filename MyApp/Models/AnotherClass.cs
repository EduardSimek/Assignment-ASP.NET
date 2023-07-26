using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyApp.Interfaces;

namespace MyApp.Models
{


   /* public class AnotherClassWithoutDI: DbContext   //without DI
    {
        public DbSet<Users> Users { get; set; }
        public static string AccessToDB => GetConnection();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(AccessToDB);
        }

        private static string GetConnection()
        {
            //string connectionToDB = @"Data Source=C:\Atom\MyDatabase.db";
            return ExternalClass.GetConnection();
        }
    }
   */
   

    public interface IClassDBContext  //with DI
    {
        DbSet<Users> Users { get; set; }
        void SaveChanges();
    }

    public class AnotherClass : DbContext, IClassDBContext  //with DI
    {
        public DbSet<Users> Users { get; set; }
        public static string AccessToDB => GetConnection();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(AccessToDB);
        }

        private static string GetConnection()
        {
            return ExternalClass.GetConnection();
        }

        void IClassDBContext.SaveChanges()
        {
            SaveChanges();
        }
    }
   

   
}

using ls.Models;
using ls.Services;
using Microsoft.EntityFrameworkCore;

namespace ls.Data
{
    public class Repository : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tarif> Tarifs { get; set; }
        public DbSet<Profit> Profits { get; set; }
        public Repository(DbContextOptions<Repository> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarif>().HasData(
                new Tarif { Id = 1, Name = "Капремонт", Val = 12.5 });
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, NumBill = "0001112223330001", Area = 36.689, GAR = "46a7ddb1-e7e1-4907-a942-54938826423c" },
                new Room() { Id = 2, NumBill = "0001112223330002", Area = 72.278, GAR = "1afb33b8-e217-4a3d-8819-0a81495ab807" },
                new Room() { Id = 3, NumBill = "0001112223330003", Area = 41.509, GAR = "ba4e473d-b83d-476f-834d-27c319be01aa" });
        }
    }

}

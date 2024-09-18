using Microsoft.EntityFrameworkCore;

namespace CourseWeb.Models
{
    public class CardContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public CardContext(DbContextOptions<CardContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>().HasData(
                new Card { Id = 1, Front = "Привет", Back = "Hello", Priority = 1 },
                new Card { Id = 2, Front = "Мир", Back = "World", Priority = 1 },
                new Card { Id = 3, Front = "Кот", Back = "Cat", Priority = 1 },
                new Card { Id = 4, Front = "Работа", Back = "Work", Priority = 1 },
                new Card { Id = 5, Front = "Дом", Back = "Home", Priority = 1 }
            );
        }
    }
}
namespace ConestogaVirtualGameStore.Tests
{
    using System;
    using System.ComponentModel;
    using Microsoft.EntityFrameworkCore;
    using Web.Data;
    using Web.Models;

    public class DatabaseFixture : IDisposable
    {
        public void Dispose()
        {
            this.context.Dispose();
        }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            this.context = new ApplicationDbContext(options);

            this.context.Games.Add(new Game { Title = "Game1", Description = "Description 1", Price = 100, Date = DateTime.Now, Developer = "Developer 1", Publisher = "Publisher 1", ImageFileName = "", Reviews = null });
            this.context.Games.Add(new Game { Title = "Game2", Description = "Description 2", Price = 100, Date = DateTime.Now, Developer = "Developer 2", Publisher = "Publisher 2", ImageFileName = "", Reviews = null });
            this.context.Games.Add(new Game { Title = "Game3", Description = "Description 3", Price = 100, Date = DateTime.Now, Developer = "Developer 3", Publisher = "Publisher 3", ImageFileName = "", Reviews = null });
            this.context.Games.Add(new Game { Title = "Game4", Description = "Description 4", Price = 100, Date = DateTime.Now, Developer = "Developer 4", Publisher = "Publisher 4", ImageFileName = "", Reviews = null });
            this.context.Games.Add(new Game { Title = "Game5", Description = "Description 5", Price = 100, Date = DateTime.Now, Developer = "Developer 5", Publisher = "Publisher 5", ImageFileName = "", Reviews = null });

            this.context.Events.Add(new Event {Title = "Event1", Description = "Description1", Date = DateTime.Now});
            this.context.Events.Add(new Event { Title = "Event2", Description = "Description2", Date = DateTime.Now });
            this.context.Events.Add(new Event { Title = "Event3", Description = "Description3", Date = DateTime.Now });
            this.context.Events.Add(new Event { Title = "Event4", Description = "Description4", Date = DateTime.Now });
            this.context.Events.Add(new Event { Title = "Event5", Description = "Description5", Date = DateTime.Now });

            this.context.SaveChanges();
        }

        public readonly ApplicationDbContext context;
    }
}

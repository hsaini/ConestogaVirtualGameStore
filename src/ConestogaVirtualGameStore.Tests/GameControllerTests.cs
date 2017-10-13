namespace ConestogaVirtualGameStore.Tests
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Web.Controllers;
    using Web.Models;
    using Xunit;

    public class GameControllerTests : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public void Index_ReturnsAllGame_AllGamesAreReturned()
        {
            using (var controller = new GameController(this.fixture.context))
            {
                var result = controller.Index().Result as ViewResult;

                Assert.NotNull(result);
                Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
            }
        }

        [Fact]
        public void Details_ReturnOneGame_OneGameIsReturned()
        {
            using (var controller = new GameController(this.fixture.context))
            {
                var result = controller.Details(1).Result as ViewResult;

                Assert.NotNull(result);
                Assert.NotNull(result.Model);

                var model = result.Model as Game;

                Assert.Equal("Game1", model.Title);
            }
        }

        [Fact]
        public void Create_CreateAGame_OneGameIsCreated()
        {
            using (var controller = new GameController(this.fixture.context))
            {
                var game = new Game
                {
                    Title = "Game6",
                    Description = "Description 6",
                    Price = 100,
                    Developer = "Developer 6",
                    Publisher = "Publisher 6",
                    ImageFileName = "",
                    Date = DateTime.Now
                };

                var result = controller.Create(game).Result as ViewResult;

                var resultGame = this.fixture.context.Games.FirstOrDefault(g => g.Title == "Game6");

                Assert.NotNull(resultGame);
                Assert.Equal("Game6", resultGame.Title);
            }
        }

        [Fact]
        public void Edit_EditAGame_OneGameIsEdited()
        {
            using (var controller = new GameController(this.fixture.context))
            {
                var resultGame = this.fixture.context.Games.FirstOrDefault(g => g.Title == "Game2");

                Assert.NotNull(resultGame);

                resultGame.Description = "Description Changed";

                var result = controller.Edit(resultGame.RecordId, resultGame).Result as ViewResult;
                
                Assert.Equal("Description Changed", resultGame.Description);
            }
        }

        [Fact]
        public void DeleteConfirmed_DeleteAGame_OneGameIsDeleted()
        {
            using (var controller = new GameController(this.fixture.context))
            {
                var result = controller.DeleteConfirmed(3).Result as ViewResult;

                var games = this.fixture.context.Games.ToList();

                Assert.Equal(5, games.Count);
            }
        }

        public GameControllerTests(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        private readonly DatabaseFixture fixture;
    }
}

namespace ConestogaVirtualGameStore.Tests
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Web.Controllers;
    using Web.Models;
    using Xunit;
    using System.Threading.Tasks;

    public class EventControllerTests : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public void Index_ReturnsAllEvents_AllEventsAreReturned()
        {

            using (var controller = new EventController(this.fixture.context))
            {
                var result = controller.Index().Result as ViewResult;

                Assert.NotNull(result);
                Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
            }
        }

        [Fact]
        public void Details_ReturnOneEvent_OneEventIsReturned()
        {
            Task.Run(() =>
            {
                using (var controller = new EventController(this.fixture.context))
                {
                    var result = controller.Details(1).Result as ViewResult;

                    Assert.NotNull(result);
                    Assert.NotNull(result.Model);

                    var model = result.Model as Event;

                    Assert.Equal("Event1", model.Title);
                }
            }).Wait();
        }

        [Fact]
        public void Create_CreateAnEvent_OneEventIsCreated()
        {
            using (var controller = new EventController(this.fixture.context))
            {
                var createEvent = new Event
                {
                    Title = "Event6",
                    Description = "Description 6",
                    Date = DateTime.Now
                };

                var result = controller.Create(createEvent).Result as ViewResult;

                var resultEvent = this.fixture.context.Events.FirstOrDefault(g => g.Title == "Event6");

                Assert.NotNull(resultEvent);
                Assert.Equal("Event6", resultEvent.Title);
            }
        }

        [Fact]
        public void Edit_EditAnEvent_OneEventIsEdited()
        {
            using (var controller = new EventController(this.fixture.context))
            {
                var resultEvent = this.fixture.context.Events.FirstOrDefault(g => g.Title == "Event2");

                Assert.NotNull(resultEvent);

                resultEvent.Description = "Description Changed";

                var result = controller.Edit(resultEvent.RecordId, resultEvent).Result as ViewResult;

                Assert.Equal("Description Changed", resultEvent.Description);
            }
        }

        [Fact]
        public void DeleteConfirmed_DeleteAnEvent_OneEventIsDeleted()
        {

            Task.Run(() =>
            {
                using (var controller = new EventController(this.fixture.context))
                {
                    Assert.Equal(5, this.fixture.context.Events.Count());
                    var result = controller.DeleteConfirmed(3).Result as ViewResult;
                    Assert.Equal(4, this.fixture.context.Events.Count());
                    var event3 = this.fixture.context.Events.FirstOrDefault(e => e.RecordId == 3);
                    Assert.Null(event3);
                }
            }).Wait();

        }

        public EventControllerTests(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        private readonly DatabaseFixture fixture;
    }
}

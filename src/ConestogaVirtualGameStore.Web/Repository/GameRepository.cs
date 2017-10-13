namespace ConestogaVirtualGameStore.Web.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;

    public class GameRepository : IGameRepository
    {
        public List<Game> GetGames()
        {
            return this.context.Games.ToList();
        }

        public Game GetGame(long id)
        {
            return this.context.Games.SingleOrDefault(m => m.RecordId == id);
        }

        public void AddGame(Game game)
        {
            this.context.Games.Add(game);
        }

        public void UpdateGame(Game game)
        {
            this.context.Games.Update(game);
        }

        public void RemoveGame(Game game)
        {
            this.context.Games.Remove(game);
        }

        public bool Exists(long id)
        {
            return this.context.Games.FirstOrDefault(g => g.RecordId == id) != null;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public GameRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private readonly ApplicationDbContext context;
    }
}

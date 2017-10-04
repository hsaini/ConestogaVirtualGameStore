namespace ConestogaVirtualGameStore.Presentation.Models
{
    using System;
    using System.Collections.Generic;

    public class Game : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }

        public IList<Review> Reviews { get; set; }
    }
}

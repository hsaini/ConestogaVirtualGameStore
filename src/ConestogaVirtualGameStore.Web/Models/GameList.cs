using System;
using System.ComponentModel.DataAnnotations;

namespace ConestogaVirtualGameStore.Web.Models
{
    public class GameList : BaseModel
    {
        public string Title { get; set; }
        public int Rating { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM}")]
        public DateTime Date { get; set; }

    }
}

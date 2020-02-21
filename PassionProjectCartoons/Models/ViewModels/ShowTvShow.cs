using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectCartoons.Models.ViewModels
{
    public class ShowTvShow
    {
        public virtual TvShow tvshow { get; set; }
        public List<Channel> channel { get; set; }

        public List<Cartoon> cartoons { get; set; }
        //Dropdown of list of all cartoons
        //We can add cartoon to the tvshow
        public List<Cartoon> all_cartoons { get; set; }
    }
}
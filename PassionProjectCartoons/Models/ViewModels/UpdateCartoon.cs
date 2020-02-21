using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectCartoons.Models.ViewModels
{
    public class UpdateCartoon
    {
        public Cartoon Cartoon { get; set; }
        public List<TvShow> TvShows { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectCartoons.Models.ViewModels
{
    public class ShowCartoon
    {
        public virtual Cartoon cartoon { get; set; }
        public List<TvShow> tvshows { get; set; }
    }
}
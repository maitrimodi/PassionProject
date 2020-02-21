using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectCartoons.Models.ViewModels
{
    public class UpdateTvShow
    {
        public TvShow TvShow { get; set; }
        public List<Channel> Channel { get; set; }
        
    }
}
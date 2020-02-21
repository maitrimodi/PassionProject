using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PassionProjectCartoons.Data
{
    public class CartoonContext : DbContext
    {
        public CartoonContext() : base("name=CartoonContext")
        { 
        }

        public System.Data.Entity.DbSet<PassionProjectCartoons.Models.TvShow> TvShows { get; set; }
        public System.Data.Entity.DbSet<PassionProjectCartoons.Models.Cartoon> Cartoons { get; set; }
        public System.Data.Entity.DbSet<PassionProjectCartoons.Models.Channel> Channels { get; set; }
        public System.Data.Entity.DbSet<PassionProjectCartoons.Models.TvShowChannel> TvshowsChannels { get; set; }
        public System.Data.Entity.DbSet<PassionProjectCartoons.Models.CartoonTvShow> CartoonTvshows{ get; set; }
    }
}
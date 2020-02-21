using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectCartoons.Models
{
    public class TvShowChannel
    {
        [Key]
        public int  TvShowChannelID { get; set; }
        public int  TvShowID { get; set; }
        [ForeignKey("TvShowID")]
        public virtual TvShow Tvshow { get; set; }
        public int? ChannelID { get; set; }
        [ForeignKey("ChannelID")]

        public virtual Channel Channel { get; set; }
        


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectCartoons.Models
{
    public class TvShow
    {
        /*
            TvShow is channeled on a particular channel
            TvShow has some properties such as:
                -Name
                -Director
                -ReleaseDate
                -Episodes
                -Genre
                -Language
            TvShow must reference a Channel
         */
        [Key]
         public int TvShowID { get; set; }
         public string TvShowName { get; set; }
         public string Director { get; set; }
         public DateTime ReleaseDate { get; set; }
         public int Episodes { get; set; }
         public string Genre { get; set; }
         public string Language { get; set; }

         public int ChannelID { get; set; }
        [ForeignKey("ChannelID")]
        public virtual Channel Channels { get; set; }
    }
}
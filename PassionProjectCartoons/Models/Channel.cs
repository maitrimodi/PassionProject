using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectCartoons.Models
{
    public class Channel
    {
        /*
          Channel contains tvshows
          Properties which define channel are:
            -Name
            -Description
            -Country
            -Owner
         */
        [Key]
        public int ChannelID { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDescription { get; set; }
        public string Country { get; set; }
        public string ChannelOwner { get; set; }
    }
}
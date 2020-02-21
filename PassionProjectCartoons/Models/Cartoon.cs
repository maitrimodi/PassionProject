using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectCartoons.Models
{
    public class Cartoon
    {
        /*
            Cartoon characters play role in tv shows which are shown on particular cartoon channel
            Properties that define cartoon are:
                -Name
                -Gender
                -Weight
                -Height
                -TvShow
            Cartoon must reference a TvShow
        */
        [Key]
        public int CartoonID { get; set; }
        public string CartoonName { get; set; }
        public string CartoonGender { get; set; }
        public string CartoonType { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int HasPic { get; set; }
        public string PicExtension { get; set; }

        //Represents many cartoons in one tv show
        public  int TvShowID { get; set; }
        [ForeignKey("TvShowID")]
        public virtual TvShow TvShows { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectCartoons.Models
{
    public class CartoonTvShow
    {
        [Key]
        public int CartoonTvShowID { get; set; }
        public int CartoonID { get; set; }
        [ForeignKey("CartoonID")]

        public virtual Cartoon Cartoon { get; set; }
        public int? TvShowID { get; set; }
        [ForeignKey("TvShowID")]
        public virtual TvShow TvShow { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using PassionProjectCartoons.Data;
using PassionProjectCartoons.Models;
using PassionProjectCartoons.Models.ViewModels;
using System.Diagnostics;
using System.IO;


namespace PassionProjectCartoons.Controllers
{
    public class TvShowController : Controller
    {
        private CartoonContext db = new CartoonContext();
        // GET: TvShow
        public ActionResult List()
        {
            List<TvShow> tvshows = db.TvShows.SqlQuery("Select * from TvShows").ToList();
            return View(tvshows);
        }

        public ActionResult Show(int id)
        {
            //data about individual tv show
            string main_query = "select * from tvshows where TvShowID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            TvShow selectedtvshow = db.TvShows.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //data about all cartoons which are in this tv show through id
            string aside_query = "Select * from Cartoons inner join CartoonTvShows on Cartoons.CartoonID = CartoonTvShows.CartoonID where CartoonTvShows.TvShowID=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<Cartoon> CartoonsInShow = db.Cartoons.SqlQuery(aside_query, fk_parameter).ToList();

            string all_cartoons_query = "Select * from Cartoons";
            List<Cartoon> AllCartoons = db.Cartoons.SqlQuery(all_cartoons_query).ToList();

            ShowTvShow viewmodel = new ShowTvShow();
            viewmodel.tvshow = selectedtvshow;
            viewmodel.cartoons = CartoonsInShow;
            viewmodel.all_cartoons = AllCartoons;
            List<Channel> channel = db.Channels.SqlQuery("Select *  from Channels").ToList();
            viewmodel.channel = channel;
            return View(viewmodel);
        }

        //This method inserts data into the bridge table
        [HttpPost]
        public ActionResult AddCartoon(int id, int CartoonID)
        {
            Debug.WriteLine("tvshow id is" + id + "and cartoonid is " + CartoonID);

            //checking if the cartoon is already in the tvshow
            string check_query = "select * from Cartoons, CartoonTvShows where CartoonTvShows.CartoonID =  Cartoons.CartoonID AND CartoonTvShows.CartoonID=@CartoonID AND CartoonTvShows.TvShowID=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@CartoonID", CartoonID);
            List<Cartoon> cartoons = db.Cartoons.SqlQuery(check_query, check_params).ToList();
            if(cartoons.Count <= 0)
            {
                string query = "insert into CartoonTvShows (CartoonID,TvShowID) values (@CartoonID,@id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@CartoonID", CartoonID);

                db.Database.ExecuteSqlCommand(query, sqlparams);
            }
            return RedirectToAction("Show/" + id);
        }

        //This method will remove data from bridge table
        [HttpGet]
        public ActionResult RemoveCartoon(int id, int CartoonID)
        {
            Debug.WriteLine("tvshow id is " + id + "and cartoon id is" + CartoonID);

            string query = "delete from CartoonTvShows where CartoonID=@CartoonID and TvShowID=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@CartoonID", CartoonID);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("Show/" + id);
        }

        [HttpPost]
        public ActionResult Add(string TvShowName, string Director, DateTime ReleaseDate, int Episodes, string Genre,string ChannelID, string Language)
        {
            string query = "insert into tvshows(TvShowName, Director, ReleaseDate, Episodes, Genre, ChannelID, Language) values (@TvShowName, @Director, @ReleaseDate, @Episodes, @Genre, @ChannelID, @Language )";
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@TvShowName", TvShowName);
            sqlparams[1] = new SqlParameter("@Director", Director);
            sqlparams[2] = new SqlParameter("@ReleaseDate", ReleaseDate);
            sqlparams[3] = new SqlParameter("@Episodes", Episodes);
            sqlparams[4] = new SqlParameter("@Genre", Genre);
            sqlparams[5] = new SqlParameter("@ChannelID", ChannelID);
            sqlparams[6] = new SqlParameter("@Language", Language);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Add()
        {
            List<Channel> channel = db.Channels.SqlQuery("Select * from Channels").ToList();
            return View(channel);
        }

        public ActionResult New()
        {
            List<TvShow> tvshow = db.TvShows.SqlQuery("Select * from TvShows").ToList();
            return View(tvshow);
        }

        public ActionResult Update(int id)
        {
            string query = "select * from TvShows where TvShowID = @TvShowID";
            var parameter = new SqlParameter("@TvShowID", id);
            TvShow selectedtvshow = db.TvShows.SqlQuery(query, parameter).FirstOrDefault();
            UpdateTvShow updateTvShow = new UpdateTvShow();
            updateTvShow.TvShow = selectedtvshow;
            List<Channel> channels = db.Channels.SqlQuery("Select * from Channels").ToList();
            updateTvShow.Channel = channels;
            return View(updateTvShow);
        }

        [HttpPost]
        public ActionResult Update(int id, string TvShowName, string Director, DateTime ReleaseDate, int Episodes, string Genre, string Language, int ChannelID)
        {
            string query = "update TvShows set TvShowName = @TvShowName, Director = @Director, ReleaseDate = @ReleaseDate, Episodes = @Episodes, Genre = @Genre, Language = @Language, ChannelID = @ChannelID where TvShowID = @TvShowID";
            SqlParameter[] sqlparams = new SqlParameter[8];
            sqlparams[0] = new SqlParameter("@TvShowName",TvShowName);
            sqlparams[1] = new SqlParameter("@Director",Director);
            sqlparams[2] = new SqlParameter("@ReleaseDate",ReleaseDate);
            sqlparams[3] = new SqlParameter("@Episodes",Episodes);
            sqlparams[4] = new SqlParameter("@Genre",Genre);
            sqlparams[5] = new SqlParameter("@Language",Language);
            sqlparams[6] = new SqlParameter("@ChannelID",ChannelID);
            sqlparams[7] = new SqlParameter("@TvShowID",id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from tvshows where TvShowID = @TvShowID";
            SqlParameter param = new SqlParameter("@TvShowID", id);
            TvShow selectedtvshow = db.TvShows.SqlQuery(query, param).FirstOrDefault();
            return View(selectedtvshow);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from tvshows where TvShowID = @TvShowID";
            SqlParameter param = new SqlParameter("@TvShowID", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
    }
}
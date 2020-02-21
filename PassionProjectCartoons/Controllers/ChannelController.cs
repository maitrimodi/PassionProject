using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassionProjectCartoons.Data;
using PassionProjectCartoons.Models;
//using PassionProjectCartoons.Models.ViewModels;
using System.Diagnostics;
using System.IO;

namespace PassionProjectCartoons.Controllers
{
    public class ChannelController : Controller
    {
        private CartoonContext db = new CartoonContext();
        // GET: Channel
        public ActionResult List()
        {
            List<Channel> channels = db.Channels.SqlQuery("Select * from Channels ").ToList();
            return View(channels);
        }

        [HttpPost]
        public ActionResult Add(string ChannelName, string ChannelDescription, string Country, string ChannelOwner)
        {
            //Debug.WriteLine(Country);
            string query = "insert into channels(ChannelName, ChannelDescription, Country,  ChannelOwner) values (@ChannelName, @ChannelDescription, @Country, @ChannelOwner)";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@ChannelName", ChannelName);
            sqlparams[1] = new SqlParameter("@ChannelDescription", ChannelDescription);
            sqlparams[2] = new SqlParameter("@Country", Country);
            sqlparams[3] = new SqlParameter("@ChannelOwner", ChannelOwner);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            string query = "select * from Channels where ChannelId=@ChannelID";
            var parameter = new SqlParameter("@ChannelID", id);
            Channel channel = db.Channels.SqlQuery(query, parameter).FirstOrDefault();
            return View(channel);
        }
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult New()
        {
            List<Channel> channel = db.Channels.SqlQuery("Select *from Channels").ToList();
            return View(channel);
        }

        public ActionResult Update(int id)
        {
            string query = "select * from Channels where ChannelId=@ChannelID";
            var parameter = new SqlParameter("@ChannelID", id);
            Channel channel = db.Channels.SqlQuery(query, parameter).FirstOrDefault();
            return View(channel);
        }
        [HttpPost]
        public ActionResult Update(int id, string ChannelName, string ChannelDescription, string Country, string ChannelOwner)
        {
            string query = "update Channels set ChannelName = @ChannelName, ChannelDescription=@ChannelDescription, Country=@Country, ChannelOwner=@ChannelOwner where ChannelID = @ChannelID";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@ChannelID",id);
            sqlparams[1] = new SqlParameter("@ChannelName",ChannelName);
            sqlparams[2] = new SqlParameter("@ChannelDescription",ChannelDescription);
            sqlparams[3] = new SqlParameter("@Country",Country);
            sqlparams[4] = new SqlParameter("@ChannelOwner",ChannelOwner);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Channels where ChannelID=@ChannelID";
            SqlParameter param = new SqlParameter("@ChannelID", id);
            Channel channel = db.Channels.SqlQuery(query, param).FirstOrDefault();
            return View(channel);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from  Channels where ChannelID = @ChannelID";
            SqlParameter param = new SqlParameter("@ChannelID", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");

        }
    }
}
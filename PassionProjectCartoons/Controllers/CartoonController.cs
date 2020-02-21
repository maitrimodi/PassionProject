/*reference from Christine Bittle Class Example "https://github.com/christinebittle/PetGroomingMVC"*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PassionProjectCartoons.Data;
using PassionProjectCartoons.Models;
using PassionProjectCartoons.Models.ViewModels;
using System.Diagnostics;
using System.IO;

namespace PassionProjectCartoons.Controllers
{
    public class CartoonController : Controller
    {
        private CartoonContext db = new CartoonContext();

        // GET: Cartoon
        public ActionResult List()
        {
            List<Cartoon> cartoons = db.Cartoons.SqlQuery("Select * from Cartoons").ToList();
            return View(cartoons);
        }

        public ActionResult Show(int id)
        {
            string query = "select * from cartoons where CartoonID = @CartoonID";
            var parameter = new SqlParameter("@CartoonID", id);
            Cartoon selectedcartoon = db.Cartoons.SqlQuery(query, parameter).FirstOrDefault();
            ShowCartoon showCartoon = new ShowCartoon();
            showCartoon.cartoon = selectedcartoon;
            List<TvShow> tvshows = db.TvShows.SqlQuery("Select * from TvShows").ToList();
            showCartoon.tvshows = tvshows;
            return View(showCartoon);
            
        }   

        //[HttpPost] is used when a form is submitted on the following URL
        //URL:/Cartoon/Add
        [HttpPost]
        public ActionResult Add(string CartoonName, string CartoonGender, string CartoonType, double Weight, double Height, string TvShowID)
        {
            Debug.WriteLine("Create a cartoon with name " + CartoonName + "and gender " + CartoonGender);
            string query = "insert into cartoons (CartoonName, CartoonGender, CartoonType, Weight, Height, TvShowID, HasPic) values (@CartoonName,@CartoonGender,@CartoonType,@Weight,@Height,@TvShowID,'')";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@CartoonName", CartoonName);
            sqlparams[1] = new SqlParameter("@CartoonGender", CartoonGender);
            sqlparams[2] = new SqlParameter("@CartoonType", CartoonType);
            sqlparams[3] = new SqlParameter("@Weight", Weight);
            sqlparams[4] = new SqlParameter("@Height", Height);
            sqlparams[5] = new SqlParameter("@TvShowID", TvShowID);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Add()
        {
            List<TvShow> tvshow= db.TvShows.SqlQuery("Select * from TvShows").ToList();
            return View(tvshow);
        }
        public ActionResult New()
        {
            List<TvShow> tvshow = db.TvShows.SqlQuery("Select * from Cartoons").ToList();

            return View(tvshow);
        }

        public ActionResult Update(int id)
        {
            Debug.WriteLine("CartoonID");
            Debug.WriteLine(id);
            Cartoon selectedcartoon = db.Cartoons.SqlQuery("select * from cartoons where CartoonID = @CartoonID", new SqlParameter("@CartoonID", id)).FirstOrDefault();
            // List<TvShow> TvShows = db.TvShows.SqlQuery("select * from tvshows").ToList();
            //UpdateCartoon UpdateCartoonViewModel = new UpdateCartoon();
            //UpdateCartoonViewModel.Cartoon = selectedcartoon;
            //UpdateCartoonViewModel.TvShows = TvShows;
            //var parameter = new SqlParameter("@CartoonID", cartoonid);
            UpdateCartoon updateCartoon = new UpdateCartoon();
            updateCartoon.Cartoon = selectedcartoon;
            List<TvShow> tvshows = db.TvShows.SqlQuery("Select * from TvShows").ToList();
            updateCartoon.TvShows = tvshows;
            return View(updateCartoon);
        }
        [HttpPost]
        public ActionResult Update(int id, string CartoonName, string CartoonGender, string CartoonType,double Weight,double Height, int TvShowID, HttpPostedFileBase CartoonPic)
        {
            int haspic = 0;
            string cartoonpicextension = "";
            
            if (CartoonPic != null)
            {
                Debug.WriteLine("Something identified...");
                //checking to see if the file size is greater than 0 (bytes)
                if (CartoonPic.ContentLength > 0)
                {
                    Debug.WriteLine("Successfully Identified Image");
                    //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
                    var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                    var extension = Path.GetExtension(CartoonPic.FileName).Substring(1);

                    if (valtypes.Contains(extension))
                    {
                        try
                        {
                            //file name is the id of the image
                            string fn = id + "." + extension;

                            //get a direct file path to ~/Content/Pets/{id}.{extension}
                            string path = Path.Combine(Server.MapPath("~/Content/Cartoons/"), fn);

                            //save the file
                            CartoonPic.SaveAs(path);
                            //if these are all successful then we can set these fields
                            haspic = 1;
                            cartoonpicextension = extension;

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Cartoon Image was not saved successfully.");
                            Debug.WriteLine("Exception:" + ex);
                        }



                    }
                }
            }

            //Debug.WriteLine("I am trying to edit a pet's name to "+PetName+" and change the weight to "+PetWeight.ToString());

            string query = "update cartoons set CartoonName=@CartoonName, CartoonGender=@CartoonGender, CartoonType=@CartoonType, Weight=@Weight,Height=@Height,TvShowID=@TvShowID, HasPic=@haspic, PicExtension=@cartoonpicextension where CartoonID=@CartoonID";
            SqlParameter[] sqlparams = new SqlParameter[9];
            sqlparams[0] = new SqlParameter("@CartoonName", CartoonName);
            sqlparams[1] = new SqlParameter("@CartoonGender", CartoonGender);
            sqlparams[2] = new SqlParameter("@CartoonType", CartoonType);
            sqlparams[3] = new SqlParameter("@Weight", Weight);
            sqlparams[4] = new SqlParameter("@Height", Height);
            sqlparams[5] = new SqlParameter("@TvShowID", TvShowID);
            sqlparams[6] = new SqlParameter("@HasPic", haspic);
            sqlparams[7] = new SqlParameter("@cartoonpicextension", cartoonpicextension);
            sqlparams[8] = new SqlParameter("@CartoonID", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the pet in the database goes here
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from cartoons where CartoonID=@CartoonId";
            SqlParameter param = new SqlParameter("@CartoonID", id);
            Cartoon selectedcartoon = db.Cartoons.SqlQuery(query, param).FirstOrDefault();
            return View(selectedcartoon);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from cartoons where CartoonID=@CartoonID";
            SqlParameter param = new SqlParameter("@CartoonID", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
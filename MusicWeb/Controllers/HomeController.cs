using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace MusicWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult UploadNhac()
        {
            
            List<BaiHat> audiolist = new List<BaiHat>();
            string CS = ConfigurationManager.ConnectionStrings["MusicContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetAllAudioFile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    BaiHat audio = new BaiHat();
                    audio.idbaihat = rdr["idbaihat"].ToString();
                    audio.Tenbaihat = rdr["Tenbaihat"].ToString();
                    audio.linkbaihat = rdr["linkbaihat"].ToString();
                    audio.casi = rdr["casi"].ToString();
                    audio.Hinhbaihat = rdr["Hinhbaihat"].ToString();
                    audiolist.Add(audio);
                }
            }
            return View(audiolist);
        }
        [HttpPost]
        public ActionResult UploadNhac(HttpPostedFileBase fileupload, string artist, HttpPostedFileBase pictureupload)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (fileupload != null)
                {
                    string fileName = Path.GetFileName(fileupload.FileName);
                    int fileSize = fileupload.ContentLength;
                    int Size = fileSize / 1000000;
                    fileupload.SaveAs(Server.MapPath("~/NHAC/" + fileName));

                    string pictureName = "";
                    if (pictureupload != null)
                    {
                        pictureName = Path.GetFileName(pictureupload.FileName);
                        pictureupload.SaveAs(Server.MapPath("~/HINH/" + pictureName));
                    }

                    string CS = ConfigurationManager.ConnectionStrings["MusicContext"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("spAddNewAudioFile", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@Tenbaihat", fileName);
                        cmd.Parameters.AddWithValue("@Linkbaihat", "~/NHAC/" + fileName);
                        cmd.Parameters.AddWithValue("@Hinhbaihat", "~/HINH/" + pictureName);
                        cmd.Parameters.AddWithValue("@casi", artist);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("UploadNhac");
            }
            
        }
        [HttpPost]
        public ActionResult DeleteAudio(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string CS = ConfigurationManager.ConnectionStrings["MusicContext"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteAudioFile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    // Get the file paths of the picture and music files to be deleted
                    string query = "SELECT Hinhbaihat, linkbaihat FROM BaiHat WHERE idbaihat=@id";
                    using (SqlCommand selectCmd = new SqlCommand(query, con))
                    {
                        selectCmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        SqlDataReader reader = selectCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string hinhPath = Server.MapPath(reader["Hinhbaihat"].ToString());
                            string nhacPath = Server.MapPath(reader["linkbaihat"].ToString());

                            // Delete the files from the file system
                            System.IO.File.Delete(hinhPath);
                            System.IO.File.Delete(nhacPath);
                        }
                        reader.Close();
                    }

                    // Delete the record from the database
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("UploadNhac");
            }
            
        }

    }


}
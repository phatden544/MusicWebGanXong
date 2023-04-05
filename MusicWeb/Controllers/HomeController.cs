using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using MusicWeb.Models;

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
                    audio.idbaihat = (int)rdr["idbaihat"];
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
                        string pictureExtension = Path.GetExtension(pictureName);
                        pictureName = Path.ChangeExtension(pictureName, pictureExtension.ToLower());
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
       
        
        [HttpGet]
        public ActionResult CreatePlaylist()
        {

            List<Playlist> playlist = new List<Playlist>();
            string CS = ConfigurationManager.ConnectionStrings["MusicContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetAllPlayList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Playlist list = new Playlist();
                    list.idPlaylist = (int)rdr["idPlaylist"];
                    list.tenplaylist = rdr["tenplaylist"].ToString();
                    list.hinh = rdr["hinh"].ToString();
                    playlist.Add(list);
                    ViewBag.Playlist = new SelectList(playlist, "idPlaylist", "tenplaylist");
                }

            }

            return View(playlist);
        }
        [HttpPost]
        public ActionResult CreatePlaylist(HttpPostedFileBase fileupload, string tenplaylist, HttpPostedFileBase pictureupload)
        {
            
            string pictureName = "";
            if (pictureupload != null)
            {
                pictureName = Path.GetFileName(pictureupload.FileName);
                pictureupload.SaveAs(Server.MapPath("~/HINH/" + pictureName));
            }

            string CS = ConfigurationManager.ConnectionStrings["MusicContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAddNewPlayList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@hinh", "~/HINH/" + pictureName);
                cmd.Parameters.AddWithValue("@tenplaylist", tenplaylist);
                cmd.ExecuteNonQuery();
            }

            ModelState.AddModelError("", "Please choose a valid image file.");

            return RedirectToAction("CreatePlaylist");
        }
        [HttpPost]
        public ActionResult DeletePlaylist(int idPlaylist)
        {
            string CS = ConfigurationManager.ConnectionStrings["MusicContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spDeletePlaylist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPlaylist", idPlaylist);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("CreatePlaylist");
        }
        [HttpGet]
        public ActionResult ListSongs(string searchQuery)
        {
            List<BaiHat> songsList = new List<BaiHat>();
            List<string> coverImages = new List<string>(); // tạo một mảng lưu trữ tất cả các ảnh bìa
            string CS = ConfigurationManager.ConnectionStrings["MusicContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "SELECT * FROM BaiHat";
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query = "SELECT * FROM BaiHat WHERE Tenbaihat LIKE '%" + searchQuery + "%' OR casi LIKE '%" + searchQuery + "%'";
                }
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    BaiHat song = new BaiHat();
                    song.idbaihat = (int)rdr["idbaihat"];
                    song.Tenbaihat = rdr["Tenbaihat"].ToString();
                    song.linkbaihat = rdr["linkbaihat"].ToString();
                    song.casi = rdr["casi"].ToString();
                    song.Hinhbaihat = rdr["Hinhbaihat"].ToString();
                    songsList.Add(song);
                    coverImages.Add(rdr["Hinhbaihat"].ToString()); // thêm đường dẫn ảnh bìa vào mảng coverImages
                }
            }
            ViewBag.CoverImages = coverImages; // gán mảng coverImages vào ViewBag để sử dụng trong view
            ViewBag.SearchQuery = searchQuery; // truyền từ khóa tìm kiếm vào ViewBag để sử dụng trong view
            return View(songsList);
        }

       





    }



}
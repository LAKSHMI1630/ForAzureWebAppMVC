using ForAzureWebAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForAzureWebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        //private SqlConnection GetConnection()

        //{
        //    var constr = new SqlConnectionStringBuilder();

        //    constr.DataSource = "dbserverask.database.windows.net";
        //    constr.UserID = "webappdb";
        //    constr.Password = "dbask@123";
        //    constr.InitialCatalog = "appdbask";
        //    return new SqlConnection(constr.ConnectionString);
        //}

        public ActionResult Index()
        {
           
            List<Course> list = new List<Course>();

            string sqlstmt = "select CourseID, CourseName, Rating from Course";

           // SqlConnection con = GetConnection();

            string constr = ConfigurationManager.ConnectionStrings["webappcon"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlstmt, con);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Course course = new Course() {
                        CourseID = reader.GetInt32(0),
                        CourseName = reader.GetString(1),
                        Rating = reader.GetDecimal(2)
                    };
                    list.Add(course);
                }
            }
            
            return View(list);
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
    }
}
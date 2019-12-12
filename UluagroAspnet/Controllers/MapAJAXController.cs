using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.ClassGetAjaxs;
using UluagroAspnet.CryptoClass;
using UluagroAspnet.ServiceReferenceMap;

namespace UluagroAspnet.Controllers
{
    public class MapAJAXController : Controller
    {
        public ActionResult Loader()
        {
            List<PergoData> list = new List<PergoData>();
            string connectionstring = "Server=213.154.5.139;Database=Pergo;Uid=sa;Pwd=p@ssw0rd;";
            string queryString = "select LONG_NAME, DISTANCE, SPEED, LONGITUDE, LATITUDE, DIRECTION, TERMINAL from TERMINAL where LONGITUDE != 0 and LATITUDE != 0";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        list.Add(new PergoData
                        {
                            name = reader[0],
                            distance = (decimal)reader[1],
                            speed = Convert.ToInt16(reader[2]),
                            longitude = reader[3],
                            latitude = reader[4],
                            direction = reader[5],
                            terminal = reader[6]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCarLocation(string terminal)
        {
            List<PergoData> list = new List<PergoData>();
            string connectionstring = "Server=213.154.5.139;Database=Pergo;Uid=sa;Pwd=p@ssw0rd;";
            string queryString = String.Format("select LONGITUDE, LATITUDE from TERMINAL where TERMINAL = '{0}'",terminal);

            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        list.Add(new PergoData
                        {
                            longitude = reader[0],
                            latitude = reader[1]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return Json(list.First(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowArchiveLog(string terminal, string start, string finish, string name)
        {
            List<PergoData> list = new List<PergoData>();
            string connectionstring = "Server=213.154.5.139;Database=PergoData;Uid=sa;Pwd=p@ssw0rd;";
            string queryString = String.Format("select DISTANCE, SPEED, LONGITUDE, LATITUDE, DIRECTION, TERMINAL, LOGDATE from ARCHIVE_OFFLINE where TERMINAL = '{0}' and LOGDATE > '{1}' and LOGDATE < '{2}' order by LOGDATE", terminal, start, finish);

            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        list.Add(new PergoData
                        {
                            name = name,
                            distance = (decimal)reader[0],
                            speed = Convert.ToInt16(reader[1]),
                            longitude = reader[2],
                            latitude = reader[3],
                            direction = reader[4],
                            terminal = reader[5],
                            logdate = (DateTime)reader[6]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            PergoDataVM vm = new PergoDataVM
            {
                list = list,
                sumdistance = String.Format("{0:0.00}", list.Sum(i => i.distance)),
                avgspeed = String.Format("{0:0.00}", list.Average(i => i.speed))
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        //For overriding MaxJsonLength
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}
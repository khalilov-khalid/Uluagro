using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using UluagroAspnet.ClassGetAjaxs;
using System.Data.SqlClient;
using UluagroAspnet.Models;

namespace UluagroAspnet.Controllers
{
    public class JSONController : Controller
    {
        private readonly OculusEntities db = new OculusEntities();
        // GET: JSON
        public ActionResult Davis(int id)
        {
            WebClient httpClient = new WebClient();

            string jsonData = null;

            if (id == 1)
            {
                jsonData = httpClient.DownloadString("https://api.weatherlink.com/v1/NoaaExt.json?user=001D0AF11D3E&pass=uluagro2018&apiToken=F84502DCB0D5444591A2F99EE35C34C1%27");
            }
            else if (id == 2)
            {
                jsonData = httpClient.DownloadString("https://api.weatherlink.com/v1/NoaaExt.json?user=001D0AF11D45&pass=uluagro2018&apiToken=F84502DCB0D5444591A2F99EE35C34C1%27");
            }


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AvtoLoad(int skip, string type)
        {
            List<GetAvtoNumber> result = new List<GetAvtoNumber>();
            string connectionstring = "Server=158.181.37.45;port=3306;Database=otoyol;Uid=root;Pwd=123123123;";
            string queryString = null;
            if (type == "normal")
            {
                queryString = "select * from otoyol_canli_ezleme";

                using (MySqlConnection con = new MySqlConnection(connectionstring))
                {

                    MySqlCommand cmd = new MySqlCommand(queryString, con);
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            result.Add(new GetAvtoNumber
                            {
                                plaka = (string)reader[5],
                                tarih = (DateTime)reader[4],
                                kam_no = (int)reader[3],
                                durum = reader[7]
                            });
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            else if (type == "ordered")
            {
                queryString = "select poce. *, (select count(*) from otoyol_canli_ezleme as oce where oce.plaka=poce.plaka) as n_count from otoyol_canli_ezleme poce where id_id in (select MAX(id_id) from otoyol_canli_ezleme group by plaka) order by tarih desc";

                using (MySqlConnection con = new MySqlConnection(connectionstring))
                {

                    MySqlCommand cmd = new MySqlCommand(queryString, con);
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            result.Add(new GetAvtoNumber
                            {
                                plaka = (string)reader[5],
                                tarih = (DateTime)reader[4],
                                kam_no = (int)reader[3],
                                durum = reader[7],
                                count = Convert.ToInt32(reader[10])
                            });
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            IEnumerable<GetAvtoNumber> data = result.AsEnumerable<GetAvtoNumber>();

            GetAvtoNumberViewModel vm = new GetAvtoNumberViewModel()
            {
                list = data.ToList().Skip(skip * 10).Take(10),
                pagecount = Math.Ceiling(Decimal.Divide(data.Count(), 10)),
                currentpage = skip + 1
            };

            if (type == "normal")
            {
                return PartialView("_PartialAvtoArxiv", vm);
            }
            else
            {
                return PartialView("_PartialAvtoOrdered", vm);
            }
        }

        public ActionResult AvtoSearch(int skip, string query)
        {
            List<GetAvtoNumber> result = new List<GetAvtoNumber>();
            string connectionstring = "Server=158.181.37.45;port=3306;Database=otoyol;Uid=root;Pwd=123123123;";
            var queryString = String.Format("select * from otoyol_canli_ezleme where plaka like '%{0}%'", query);

            using (MySqlConnection con = new MySqlConnection(connectionstring))
            {

                MySqlCommand cmd = new MySqlCommand(queryString, con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        result.Add(new GetAvtoNumber
                        {
                            plaka = (string)reader[5],
                            tarih = (DateTime)reader[4],
                            kam_no = (int)reader[3],
                            durum = reader[7]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            IEnumerable<GetAvtoNumber> data = result.AsEnumerable<GetAvtoNumber>();
            GetAvtoNumberViewModel vm;

            if (data.Count() <= 10)
            {
                vm = new GetAvtoNumberViewModel()
                {
                    list = data.ToList(),
                    pagecount = 0,
                    currentpage = skip + 1
                };
            }
            else
            {
                vm = new GetAvtoNumberViewModel()
                {
                    list = data.ToList().Skip(skip * 10).Take(10),
                    pagecount = Math.Ceiling(Decimal.Divide(data.Count(), 10)) - 1,
                    currentpage = skip + 1
                };
            }


            return PartialView("_PartialAvtoSearch", vm);
        }

        public ActionResult WorkerLoad(int skip, int? id, string start, string finish)
        {
            List<WorkerData> data = new List<WorkerData>();

            string connectionstring = "Server=213.154.5.139;Database=TURNIKET;Uid=sa;Pwd=p@ssw0rd;";
            string queryString;

            if (id == null && String.IsNullOrEmpty(start) && String.IsNullOrEmpty(finish))
            {
                queryString = "select distinct Workers.ID, Workers.Name, Workers.Surname, CHECKINOUT.sn, CHECKINOUT.CHECKTIME from USERINFO join [Oculus].dbo.Workers on USERINFO.BADGENUMBER = Workers.TurniketID join CHECKINOUT on USERINFO.USERID = CHECKINOUT.USERID order by CHECKTIME desc";
            }
            else if(id != null && String.IsNullOrEmpty(start) && String.IsNullOrEmpty(finish))
            {
                queryString = String.Format("select distinct Workers.ID, Workers.Name, Workers.Surname, CHECKINOUT.sn, CHECKINOUT.CHECKTIME from USERINFO join [Oculus].dbo.Workers on USERINFO.BADGENUMBER = Workers.TurniketID join CHECKINOUT on USERINFO.USERID = CHECKINOUT.USERID where Workers.ID = {0} order by CHECKTIME desc", id);
            }
            else if(id == null && !String.IsNullOrEmpty(start) && !String.IsNullOrEmpty(finish))
            {
                queryString = String.Format("select distinct Workers.ID, Workers.Name, Workers.Surname, CHECKINOUT.sn, CHECKINOUT.CHECKTIME from USERINFO join [Oculus].dbo.Workers on USERINFO.BADGENUMBER = Workers.TurniketID join CHECKINOUT on USERINFO.USERID = CHECKINOUT.USERID where CHECKTIME > '{0}' and CHECKTIME < '{1}' order by CHECKTIME desc", start, finish);
            }
            else
            {
                queryString = String.Format("select distinct Workers.ID, Workers.Name, Workers.Surname, CHECKINOUT.sn, CHECKINOUT.CHECKTIME from USERINFO join [Oculus].dbo.Workers on USERINFO.BADGENUMBER = Workers.TurniketID join CHECKINOUT on USERINFO.USERID = CHECKINOUT.USERID where Workers.ID = {0} and CHECKTIME > '{1}' and CHECKTIME < '{2}' order by CHECKTIME desc", id, start, finish);
            }

            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        data.Add(new WorkerData
                        {
                            id = reader[0],
                            name = reader[1],
                            surname = reader[2],
                            sn = reader[3],
                            checktime = (DateTime)reader[4]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            if (data.Count() != 0)
            {
                WorkerDataVM vm = new WorkerDataVM()
                {
                    list = data.Skip(skip * 10).Take(10),
                    pagecount = Math.Ceiling(Decimal.Divide(data.Count(), 10)),
                    currentpage = skip + 1
                };

                return PartialView("_PartialWorkControl", vm);
            }
            else
            {
                return Content("<div class='empty-data pt-60 pb-40 text-center text-gray bg-gray' role='presentation'><i aria-hidden='true' class='icon-info-circle-thin mb-30' style='font-size: 100px'></i><span class='d-block font-16 bold text-uppercase'>Seçimə uyğun məlumat yoxdur.</span></div>");
            }
        }

        public ActionResult GarageLoad(int skip)
        {
            IEnumerable<GarageData> data = db.TECHNIQUES.Select(t => new GarageData { name = t.NAME, condition = t.TECHNIQUE_CONDITION.NAME, type = t.TECHNIQUE_TYPE.NAME, work_type = t.TECHNIQUE_WORKING_TYPE.NAME }).ToList();
            GarageDataVM vm = new GarageDataVM()
            {
                list = data.Skip(skip * 10).Take(10),
                pagecount = Math.Ceiling(Decimal.Divide(data.Count(), 10)),
                currentpage = skip + 1
            };

            return PartialView("_PartialGarageLoad", vm);
        }

        public ActionResult LoadWorkerForProfession(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return Json(db.Workers.Where(w => w.Professions_id == id).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
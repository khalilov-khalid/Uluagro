using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Controllers
{
    [UserAuthenticationController]
    public class DocumentController : Controller
    {
        private readonly OculusEntities db = new OculusEntities();
        // GET: Document
        public ActionResult LoadWorkerForProfession(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            User currentuser = Session["UserLogin"] as User;

            return Json(db.Workers.Where(w => w.Professions_id == id && w.ID != currentuser.WorkerID).ToList(),JsonRequestBehavior.AllowGet);
        }

        public async Task<string> SendMessage(string content, string title, string note, int receiverid)
        {
            User currentuser = Session["UserLogin"] as User;

            DOCUMENT doc = new DOCUMENT
            {
                CONTENT = content,
                CREATORID = currentuser.WorkerID
            };

            DOCUMENT newdoc = null;

            Task addnewdoc = Task.Run(() =>
            {
                newdoc = db.DOCUMENTS.Add(doc);
                db.SaveChanges();
            });

            await addnewdoc;

            db.DOCMESSAGEs.Add(new DOCMESSAGE
            {
                SENDERID = currentuser.WorkerID,
                RECIVED = receiverid,
                TITLE = title,
                Note = note,
                DOCID = newdoc.OBJECTID,
                OPRTYPE = 3,
                CREATEDATE = DateTime.Now
            });
            db.SaveChanges();

            return "OK";
        }

        public ActionResult ShowRecivedMessages()
        {
            User currentuser = Session["UserLogin"] as User;
            var data = db.DOCMESSAGEs.Where(d => d.RECIVED == currentuser.WorkerID && d.OPRTYPE == 3).OrderByDescending(t => t.CREATEDATE).ToList();
            if(data.Count() != 0)
            {
                return PartialView("_PartialRecivedMessages", data);
            }
            else
            {
                return Content("<div class='empty-data pt-60 pb-40 text-center text-gray bg-gray' role='presentation'><i aria-hidden='true' class='icon-info-circle-thin mb-30' style='font-size: 100px'></i><span class='d-block font-16 bold text-uppercase'>Yeni mesaj yoxdur.</span></div>");
            }
        }

        public ActionResult ShowMessageContent(int id)
        {
            return PartialView("_PartialDocumentDetail", db.DOCMESSAGEs.FirstOrDefault(d => d.OBJECTID == id));
        }

        public ActionResult ShowSendedMessages()
        {
            User currentuser = Session["UserLogin"] as User;
            var data = db.DOCMESSAGEs.Where(d => d.SENDERID == currentuser.WorkerID && d.OPRTYPE == 3).OrderByDescending(t => t.CREATEDATE).ToList();
            if (data.Count() != 0)
            {
                return PartialView("_PartialSendedMessages", data);
            }
            else
            {
                return Content("<div class='empty-data pt-60 pb-40 text-center text-gray bg-gray' role='presentation'><i aria-hidden='true' class='icon-info-circle-thin mb-30' style='font-size: 100px'></i><span class='d-block font-16 bold text-uppercase'>Yeni mesaj yoxdur.</span></div>");
            }
        }

        public ActionResult İnferiorDocument()
        {
            ViewBag.Professions = db.Professions.Where(p => p.CanResponser == true).ToList();
            return PartialView("_PartialİnferiorDocument");
        }

        public string SendDocToMoreUsers(List<int> users, int docid)
        {
            User currentuser = Session["UserLogin"] as User;

            foreach (var item in users)
            {
                db.DOCMESSAGEs.Add(new DOCMESSAGE
                {
                    SENDERID = currentuser.WorkerID,
                    RECIVED = item,
                    TITLE = "Dərkənar",
                    DOCID = docid,
                    OPRTYPE = 3,
                    CREATEDATE = DateTime.Now
                });
                db.SaveChanges();
            }

            return "OK";
        }

        public string AcceptDoc(int doc_msg_id, string note)
        {
            User currentuser = Session["UserLogin"] as User;

            DOCMESSAGE slctd_doc = db.DOCMESSAGEs.Find(doc_msg_id);

            db.DOCMESSAGEs.Add(new DOCMESSAGE
            {
                SENDERID = currentuser.WorkerID,
                RECIVED = slctd_doc.SENDERID,
                Note = note,
                TITLE = "Qəbul",
                DOCID = slctd_doc.DOCID,
                OPRTYPE = 1,
                CREATEDATE = DateTime.Now
            });
            slctd_doc.OPRTYPE = 1;
            db.SaveChanges();

            return "OK";
        }

        public string DeclineDoc(int doc_msg_id, string note)
        {
            User currentuser = Session["UserLogin"] as User;

            DOCMESSAGE slctd_doc = db.DOCMESSAGEs.Find(doc_msg_id);

            db.DOCMESSAGEs.Add(new DOCMESSAGE
            {
                SENDERID = currentuser.WorkerID,
                RECIVED = slctd_doc.SENDERID,
                Note = note,
                TITLE = "İmtina",
                DOCID = slctd_doc.DOCID,
                OPRTYPE = 2,
                CREATEDATE = DateTime.Now
            });
            slctd_doc.OPRTYPE = 2;
            db.SaveChanges();

            return "OK";
        }

        public ActionResult ShowArchiveDocuments()
        {
            User currentuser = Session["UserLogin"] as User;

            IEnumerable<DOCMESSAGE> notwaiting = db.DOCMESSAGEs.Where(d => d.OPRTYPE != 3).OrderByDescending(t => t.CREATEDATE).ToList();

            var data = notwaiting.Where(m => m.RECIVED == currentuser.WorkerID || m.SENDERID == currentuser.WorkerID).OrderByDescending(t => t.CREATEDATE).ToList();

            if(data.Count() != 0)
            {
                return PartialView("_PartialArchiveDocuments", data);
            }
            else
            {
                return Content("<div class='empty-data pt-60 pb-40 text-center text-gray bg-gray' role='presentation'><i aria-hidden='true' class='icon-info-circle-thin mb-30' style='font-size: 100px'></i><span class='d-block font-16 bold text-uppercase'>Arxiv mesaj yoxdur.</span></div>");
            }
        }
    }
}
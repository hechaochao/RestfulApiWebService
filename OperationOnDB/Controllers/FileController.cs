using DB.Models;
using Microsoft.DocAsCode.Build.Engine;
using OperationOnDB.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationOnDB.Controllers
{
    [BasicAuthenticationAttribute("user", "user",
    BasicRealm = "localhost")]
    public class FileController : Controller
    {
        private dataEntities db = new dataEntities();

        // GET: File
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveFile()
        {
            var files = Request.Files;
            //var file = files[0];
            if (ModelState.IsValid)
            {
                
                for(int i = 0; i < files.Count; i++)
                {
                    var filePath = Server.MapPath(string.Format("~/{0}", "uploads"));
                    var fileName = files[i].FileName;
                    string fullPath = Path.Combine(filePath, fileName);
                    files[i].SaveAs(fullPath);
                    SaveData(fullPath);
                }
                
                ModelState.Clear();
                ViewBag.Message = "Upload Successfully!";
                return View();
            }
            ViewBag.Message = "Upload Failed!";
            return View();
        }

       
        private void SaveData(string path)
        {
            
            XRefMap xref = Microsoft.DocAsCode.Common.YamlUtility.Deserialize<XRefMap>(path);
            foreach (var spec in xref.References)
            {
                uidt t = new uidt();
                t.uid = spec["uid"];
                t.objectStr = Newtonsoft.Json.JsonConvert.SerializeObject(spec);
                db.uidts.Add(t);
                db.SaveChanges();
            }
        }

    }
}
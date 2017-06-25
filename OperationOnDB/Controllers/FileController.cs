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
        // GET: File
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(dynamic d)
        {
            var files = Request.Files;
            //var file = files[0];
            if (ModelState.IsValid)
            {
                
                for(int i = 0; i < files.Count; i++)
                {
                    var filePath = Server.MapPath(string.Format("~/{0}", "uploads"));
                    var fileName = files[i].FileName;
                    files[i].SaveAs(Path.Combine(filePath, fileName));
                }
                
                ModelState.Clear();
                ViewBag.Message = "Upload Successfully!";
                return View();
            }
            ViewBag.Message = "Upload Failed!";
            return View();
        }
    }
}
using Microsoft.DocAsCode.Build.Engine;
using RestfulApiService.DTOs;
using RestfulApiService.src;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using RestfulApiWebService.Models;
using System.Web.Script.Serialization;
using Microsoft.DocAsCode.Plugins;

namespace RestfulApiService.Controllers
{
    [RoutePrefix("api/uids")]
    public class TestController : ApiController
    {
        private testEntities1 db = new testEntities1();

        //private static readonly Expression<Func<uidt, UidSim>> AsUidSim =
        //    x => new UidSim
        //    {
        //        uid = x.uid,
        //        name = x.name,
        //        commentId = x.commentId,
        //        href = x.href
        //    };

        [HttpPost]
        [Route("~/api/add")]
        public void test()
        {
            string path = @"C:\Users\t-chaohe\Desktop\Workspace\RestfulApiWebService\RestfulApiService\xrefmap.yml";
            XRefMap xref = Microsoft.DocAsCode.Common.YamlUtility.Deserialize<XRefMap>(path);
            foreach(var spec in xref.References)
            {
                t1 t = new t1();
                t.uid = spec["uid"];

                JavaScriptSerializer js = new JavaScriptSerializer {

                 };
                var k = Newtonsoft.Json.JsonConvert.SerializeObject(spec);
                var kk = Newtonsoft.Json.JsonConvert.DeserializeObject<XRefSpec>(k);

                t.objectstr = js.Serialize(spec);
                db.t1.Add(t);

                XRefSpec xf;
                xf = js.Deserialize<XRefSpec>(t.objectstr);
                db.SaveChanges();
            }
        }
        //[HttpPost]
        //[Route("~/api/adduids")]
        //public void PostData()
        //{
        //    string path = @"C:\Users\t-chaohe\Desktop\Workspace\RestfulApiWebService\RestfulApiService\xrefmap.yml";

        //    try
        //    {
        //        XRefMap xref = Microsoft.DocAsCode.Common.YamlUtility.Deserialize<XRefMap>(path);

        //        foreach (var spec in xref.References)
        //        {
        //            uidt ud = new uidt();
        //            Tool.InitUidt(ud);
        //            ICollection<string> keys = spec.Keys;
        //            foreach (string str in keys)
        //            {
        //                switch (str)
        //                {
        //                    case "uid":
        //                        ud.uid = spec[str];
        //                        break;
        //                    case "name":
        //                        ud.name = spec[str];
        //                        break;
        //                    case "href":
        //                        ud.href = spec[str];
        //                        break;
        //                    case "commentId":
        //                        ud.commentId = spec[str];
        //                        break;
        //                    case "nameWithType":
        //                        ud.nameWithType = spec[str];
        //                        break;
        //                    case "nameWithType.vb":
        //                        ud.nameWithType_vb = spec[str];
        //                        break;
        //                    case "name.vb":
        //                        ud.name_vb = spec[str];
        //                        break;
        //                    case "fullName":
        //                        ud.fullName = spec[str];
        //                        break;
        //                    case "fullName.vb":
        //                        ud.fullName_vb = spec[str];
        //                        break;
        //                }
        //            }
        //            db.uidts.Add(ud);
        //            db.SaveChanges();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return;
        //}

        //[HttpGet]
        //[Route("{uid}")]
        //[ResponseType(typeof(UidSim))]
        //public async Task<IHttpActionResult> GetByUid(string uid)
        //{
        //    UidSim uds = await db.uidts.Where(b => b.uid == uid)
        //                        .Select(AsUidSim)
        //                        .FirstOrDefaultAsync();
        //    if (uds == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(uds);
        //}

        //[HttpPost]
        //[Route("")]
        //[ResponseType(typeof(UidSim))]
        //public async Task<IHttpActionResult> PostByUids([FromBody]string[] uids)
        //{
        //    List<UidSim> uds = new List<UidSim>();
        //    foreach (string uid in uids)
        //    {
        //        UidSim temp = await db.uidts.Where(b => b.uid == uid)
        //                       .Select(AsUidSim)
        //                       .FirstOrDefaultAsync();
        //        uds.Add(temp);
        //    }

        //    if (uds == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(uds);
        //}

    }
}

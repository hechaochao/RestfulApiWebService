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

using System.Web.Script.Serialization;
using Microsoft.DocAsCode.Plugins;
using DB.Models;

namespace RestfulApiService.Controllers
{
    [RoutePrefix("uids")]
    public class QueryController : ApiController
    {
        private dataEntities db = new dataEntities();

        //private static readonly Expression<Func<uidt, UidSim>> AsUidSim =
        //    x => new UidSim
        //    {
        //        uid = x.uid,
        //        name = x.name,
        //        commentId = x.commentId,
        //        href = x.href
        //    };

        [HttpGet]
        [Route("{uid}")]
        public async Task<IHttpActionResult> GetByUid(string uid)
        {
            uidt ut = await db.uidts.Where(b => b.uid == uid)
                                .Select(c => c)
                                .FirstOrDefaultAsync();
            
            if (ut == null)
            {
                return NotFound();
            }
            XRefSpec xf = Newtonsoft.Json.JsonConvert.DeserializeObject<XRefSpec>(ut.objectStr);
            return Ok(xf);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostByUids([FromBody]string[] uids)
        {
            List<uidt> uts = new List<uidt>();
            foreach (string uid in uids)
            {
                uidt temp = await db.uidts.Where(b => b.uid == uid)
                               .Select(c => c)
                               .FirstOrDefaultAsync();
                uts.Add(temp);
            }

            //if (uts == null)
            //{
            //    return NotFound();
            //}

            List<XRefSpec> xfs = new List<XRefSpec>();
            foreach(uidt ut in uts)
            {
                XRefSpec xf = Newtonsoft.Json.JsonConvert.DeserializeObject<XRefSpec>(ut.objectStr);
                xfs.Add(xf);
            }
            return Ok(xfs);
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

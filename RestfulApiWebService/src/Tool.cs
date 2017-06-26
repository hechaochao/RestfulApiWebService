using Microsoft.DocAsCode.Build.Engine;
using Microsoft.DocAsCode.Common;
using RestfulApiWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestfulApiService.src
{

    public class Tool
    {

        public static XRefMap GetXrefMap(string path)
        {
            XRefMap xref = YamlUtility.Deserialize<XRefMap>(path);
            return xref;
        }

        //public static void InitUidt(uidt ud)
        //{
        //    ud.commentId = null;
        //    ud.fullName = null;
        //    ud.fullName_vb = null;

        //    ud.nameWithType = null;
        //    ud.nameWithType_vb = null;
        //    ud.name_vb = null;
        //}
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Tool tl = new Tool();
    //        string path = "../xrefmap.yml";
    //        tl.GetXrefMap(path);
    //    }
    //}
}
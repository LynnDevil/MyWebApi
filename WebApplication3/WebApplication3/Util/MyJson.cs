using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Util
{
    /// <summary>
    /// Json格式转换
    /// </summary>
    public class MyJson
    {
        /// <summary>
        /// 单个对象转换成Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage ObjectToJson(object obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string r = js.Serialize(obj);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(r, Encoding.UTF8, "text/json")
            };
            return result;
        }
        /// <summary>
        /// 多个对象转换成Json
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static HttpResponseMessage ObjectToJson(List<object> objs)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string r = js.Serialize(objs);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(r, Encoding.UTF8, "text/json")
            };
            return result;
        }

        //public ActionResult ReviewFile(string folderName, string fileBasename, string extendName)
        //{
        //    //以后根据后缀名返回相应的文件
        //    var fileFullName = "~/" + folderName + "/" + fileBasename + extendName;
        //    var path = Server.MapPath(fileFullName);
        //    switch (extendName)
        //    {
        //        case ".html":
        //            return File(path, "text/html");
        //            break;
        //        case ".txt":
        //            return File(path, "text/plain");
        //            break;
        //        case ".doc":
        //            return File(path, "application/msword");
        //            break;
        //        case ".xls":
        //            var downLoadFileName = fileBasename + ".xls";
        //            return File(path, "application/ms-excel", downLoadFileName);
        //            //return File(path, "application/x-excel");
        //            break;
        //        case ".pff":
        //            return File(path, "application/ms-powerpoint");
        //            break;
        //        case ".pdf":
        //            return File(path, "application/pdf");
        //            break;
        //        case ".zip":
        //            return File(path, "application/zip");
        //            break;
        //        default:
        //            var noPointExtentName = extendName.Substring(extendName.IndexOf('.') + 1);
        //            return File(path, "image/" + noPointExtentName);
        //    }
        //}
    }
}

using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace YouVote.Models.Plugin
{
    public class JsonNetResult : JsonResult
    {
        public static JsonNetResult Convert(object o)
        {
            return new JsonNetResult(o);
        }

        public JsonNetResult()
        {
            
        }

        public JsonNetResult(object data)
        {
            Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented, new JsonSerializerSettings());
            response.Write(serializedObject);
        }
    }

    public class DefaultJsonObject
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
        public JsAlertType Type { get; set; }
    }

    public enum JsAlertType
    {
        Error = 1,
        Info = 2,
        Success = 3
    }
}
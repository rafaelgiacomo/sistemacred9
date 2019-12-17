using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaCred9.Web.UI.Helpers
{
    public static class JsonExtension
    {
        public static string ToJSON(this object jsonObj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string serializado = serializer.Serialize(jsonObj);
            return serializado;
        }
    }
}
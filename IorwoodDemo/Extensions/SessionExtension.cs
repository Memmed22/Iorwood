
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IorwoodDemo.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(this Microsoft.AspNetCore.Http.ISession session, string key, List<object> value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
          
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var val = session.GetString(key);
            return val == null ? default(T) : JsonConvert.DeserializeObject<T>(val);
        }
        public static void SetList(this ISession session, string key, List<object> value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

    }
}

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Utils.Parse
{
    public static class JsonUtils
    {
        public static string ToJson(this object obj) => 
            JsonConvert.SerializeObject(obj);
                    
        public static string ToJson(this (string key, object value) target)
        {
            // desired result as via dict to json
            //"{\"profile_action\":\"CropPlanted\"}"
            return "{" + $"\"{target.key}\":\"{target.value}\"" + "}";
        }
        
        public static T ToDeserialized<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return default;
            }
        }
    }
}

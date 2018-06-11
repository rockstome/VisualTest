using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public static class Helper
    {
        public static Dictionary<string, string> GetUser(string fileName)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Users\");
            string path = dir + fileName + ".json";
            if (!File.Exists(path))
            {
                throw new Exception($"File {path} does not exist.");
            }

            string text = File.ReadAllText(path);
            JObject jsonObj = JObject.Parse(text);
            var temp = jsonObj.ToObject<Dictionary<string, Dictionary<string, string>>>();
            return temp[fileName];
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Utils.Parse
{
    public static class TsvHelper
    {
        public static List<List<string>> ParseTsv(string text) => 
            Parse(text, '\t');

        private static List<List<string>> Parse(string csvText, char separator)
        {
            var result = new List<List<string>>();
            var lines = csvText.Split('\n');
            
            foreach (var line in lines)
            {
                var values = line.Split(separator);
                var lineResult = values.Select(value => value.Replace("\r", "")).ToList();

                result.Add(lineResult);
            }
            
            return result;
        }

        public static Dictionary<string, List<string>> ParseTsvWithHeaders(string text) =>
            ParseWithHeaders(text, '\t');

        private static Dictionary<string, List<string>> ParseWithHeaders(string text, char c)
        {
            var result = new Dictionary<string, List<string>>();
            var lines = text.Split('\n');
            var headers = lines[0].Split(c);
            
            for (var i = 0; i < headers.Length; i++)
            {
                var header = headers[i];
                result.Add(header, new List<string>());
            }
            
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var values = line.Split(c);
                
                for (var j = 0; j < values.Length; j++)
                {
                    var value = values[j];
                    var header = headers[j];
                    
                    result[header].Add(value.Replace("\r", ""));
                }
            }
            
            return result;
        }
    }
}
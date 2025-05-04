using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _Project.Scripts.Game.Features.Weapon.Data;
using _Project.Scripts.Infrastructure.StaticData.Configs.Data;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts.Utils.Parse
{
    public static class StringParseUtils
    {
        public static string ToString(string value) => value;
        
        public static bool ToBool(string value)
        {
            if (value.IsNullOrWhitespace() || value == "0")
                return false;

            if (value == "1")
                return true;

            return bool.Parse(value);
        }
        
        public static int ToInt(string value)
        {
            if (value.IsNullOrWhitespace())
                return 0;

            if (int.TryParse(value.Replace(" ", ""), out var result))
                return result;
            
            throw new FormatException($"Can't convert string '{value}' to int");
        }

        public static float ToFloat(string value)
        {
            var str = value.Replace(" ", "");

            if (float.TryParse(str,NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                return result;

            throw new FormatException($"Can't convert string '{value}' to float");
        }

        public static Vector2 ToVector2(string str)
        {
            var value = str.Replace("\"", string.Empty);
            var arr = value.Split(',');
            var x = ToFloat(arr[0]);
            var y = ToFloat(arr[1]);

            return new Vector2(x, y);
        }

        public static Dictionary<int, int> ToIntIntDictionary(string str)
        {
            var result = new Dictionary<int, int>();

            var pairStrings = ToStringArray(str);
            foreach (var pairString in pairStrings)
            {
                var pair =  ToIntArray(pairString, ':');
                result.Add(pair[0], pair[1]);
            }

            return result;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(string str, Func<string, TKey> keyParser, Func<string, TValue> valueParser)
        {
            var result = new Dictionary<TKey, TValue>();

            var pairStrings = ToStringArray(str);
            foreach (var pairString in pairStrings)
            {
                var pair =  ToStringArray(pairString, ':');
                result.Add(keyParser(pair[0]), valueParser(pair[1]));
            }

            return result;
        }
        
        public static int[] ToIntArray(string value, char separator = ',')
        {
            value = value.Replace("\"", string.Empty);
            
            var arr = value.Split(separator);
            var result = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = ToInt(arr[i]);
            }

            return result;
        }
        
        public static float[] ToFloatArray(string value)
        {
            value = value.Replace("\"", string.Empty);
            
            var arr = value.Split(',');
            var result = new float[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = ToFloat(arr[i]);
            }

            return result;
        }

        public static string[] ToStringArray(string value, char separator = ',', int count = -1)
        {
            value = value.Replace("\"", string.Empty);

            return count > 0 
                ? value.Split(separator, count)
                : value.Split(separator);
        }
        
        public static ConfigPrefab ToPrefab(string value) => new ConfigPrefab { Name = value };

        private static Color32 ToColor(string colorString)
        {
            float[] arr = ToFloatArray(colorString);
            return new Color32((byte)arr[0], (byte)arr[1], (byte)arr[2], (byte)arr[3]);
        }
        
        public static int ToCollisionMask(string value)
        {
            var arr = ToStringArray(value);
            int result = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                result |= 1 << (int)ToEnum<CollisionLayer>(arr[i]);
            }

            return result;
        }
        
        public static T ToJson<T>(string json) => json.ToDeserialized<T>();

        public static TEnum ToEnum<TEnum>(string value) where TEnum : Enum => EnumParser<TEnum>.Convert(value);
        
        private static class EnumParser<TEnum> where TEnum : Enum
        {
            private static readonly Dictionary<string, TEnum> SerializedValues;
        
            public static IReadOnlyCollection<TEnum> GetValues() => SerializedValues.Values;

            static EnumParser()
            {
                SerializedValues = Enum.GetNames(typeof(TEnum))
                    .ToDictionary(x => x,
                        x => (TEnum)Enum.Parse(typeof(TEnum), x), StringComparer.OrdinalIgnoreCase);
            }


            public static TEnum Convert(string value)
            {
                if (SerializedValues.TryGetValue(value, out var enumValue))
                    return enumValue;

                throw new Exception($"Enum '{typeof(TEnum)}' doesn't have a case for value: '{value}'");
            }

            public static TEnum Convert(string value, TEnum defaultValue)
            {
                return SerializedValues.TryGetValue(value, out var enumValue) ? enumValue : defaultValue;
            }

            public static List<TEnum> Convert(IEnumerable<string> values)
            {
                return values.Select(Convert).ToList();
            }

            public static bool TryConvert(string value, out TEnum result)
            {
                if (SerializedValues.TryGetValue(value, out var enumValue))
                {
                    result = enumValue;
                    return true;
                }

                result = default;
                return false;
            }
        }
    }
}
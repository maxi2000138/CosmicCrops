using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Utils.Parse;

namespace _Project.Scripts.Infrastructure.StaticData.Configs
{
    public abstract class BaseConfig<TKey, TData> : IConfigParser
    {
        public abstract string ConfigName { get; }

        public IReadOnlyDictionary<TKey, TData> Data { get; private set; }
        public TData this[TKey key] => Data[key];

        public void Parse(List<List<string>> textData)
        {
            var dict = new Dictionary<TKey, TData>();
            
            foreach (var row in textData)
            {
                var data = ParseData(row);
                var key = GetKey(data);
                dict.Add(key, data);
            }

            Data = dict;
        }

        protected abstract TKey GetKey(TData data);
        protected abstract TData ParseData(List<string> row);
        
        public TKey GetLastKey() => Data.Keys.Last();
    }
}
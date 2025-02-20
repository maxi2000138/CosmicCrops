using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Infrastructure.StaticData.Configs
{
    public abstract class BaseConfigConstants : IConfigParser
    {
        public abstract string ConfigName { get; }

        protected IReadOnlyDictionary<string, string> Data { get; private set; }
        
        public void Parse(List<List<string>> textData)
        {
            Data = textData.ToDictionary(row => row[0], row => row[1]);
        }
    }
}
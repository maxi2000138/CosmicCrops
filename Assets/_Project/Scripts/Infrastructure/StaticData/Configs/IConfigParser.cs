using System.Collections.Generic;

namespace _Project.Scripts.Infrastructure.StaticData.Configs
{
    public interface IConfigParser
    {
        string ConfigName { get; }
        void Parse(List<List<string>> data);
    }
}

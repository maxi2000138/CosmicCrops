using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Infrastructure.StaticData.Configs.Data;
using _Project.Scripts.UI.Screens;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.UI._Configs
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ScreensConfig : BaseConfig<ScreenType, ScreenData>
    {
        public override string ConfigName => "Screens";
        protected override ScreenType GetKey(ScreenData data) => data.Type;
        protected override ScreenData ParseData(List<string> row)
        {
            return new()
            {
                Type =  StringParseUtils.ToEnum<ScreenType>(row[0]),
                Prefab = StringParseUtils.ToPrefab(row[1]),
            };
        }
    }
    
    [Serializable]
    public class ScreenData
    {
        public ScreenType Type;
        public ConfigPrefab Prefab;
    }
}
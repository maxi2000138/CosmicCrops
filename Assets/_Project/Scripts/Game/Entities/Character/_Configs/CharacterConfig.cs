using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Entities.Character._Configs
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class CharacterConfig : BaseConfigConstants
    {
        public override string ConfigName => "Character";

        public int Health => _health ??= StringParseUtils.ToInt(Data["health"]);
        private int? _health;

        public int Speed => _speed ??= StringParseUtils.ToInt(Data["speed"]);
        private int? _speed;

        public string PrefabName => _prefabName ??= Data["prefab_name"];
        private string _prefabName;
    }
}
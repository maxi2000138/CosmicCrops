using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Infrastructure.StaticData.Configs.Data;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Units.Character._Configs
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class CharacterConfig : BaseConfigConstants
    {
        public override string ConfigName => "Character";

        public int Health => _health ??= StringParseUtils.ToInt(Data["health"]);
        private int? _health;

        public int Speed => _speed ??= StringParseUtils.ToInt(Data["speed"]);
        private int? _speed;

        public ConfigPrefab Prefab => _prefab ??= StringParseUtils.ToPrefab(Data["prefab_name"]);
        private ConfigPrefab _prefab;

        public float Height => _height ??= StringParseUtils.ToInt(Data["height"]);
        private float? _height;

        public string Weapon => _weapon ??= Data["weapon"];
        private string _weapon;
    }
}
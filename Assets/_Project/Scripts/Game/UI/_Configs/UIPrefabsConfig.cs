using _Project.Scripts.Infrastructure.StaticData.Configs;
using _Project.Scripts.Infrastructure.StaticData.Configs.Data;
using _Project.Scripts.Utils.Parse;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.UI._Configs
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public sealed class UIPrefabsConfig : BaseConfigConstants
  {
    public override string ConfigName => "UIPrefabs";

    public ConfigPrefab HealthViewPrefab => _healthView ??= StringParseUtils.ToPrefab(Data["prefab_health_view"]);
    private ConfigPrefab _healthView;
  }
}
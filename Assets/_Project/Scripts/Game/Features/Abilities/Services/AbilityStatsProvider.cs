using System.Collections.Generic;
using _Project.Scripts.Game.Features.Abilities._Configs;
using _Project.Scripts.Game.Features.Abilities._Configs.Data;
using JetBrains.Annotations;

namespace _Project.Scripts.Game.Features.Abilities.Services
{
  [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
  public class AbilityStatsProvider : IAbilityStatsProvider
  {
    private readonly AbilitiesConfig _abilitiesConfig;
    private readonly EffectsConfig _effectsConfig;
    public AbilityStatsProvider(AbilitiesConfig abilitiesConfig, EffectsConfig effectsConfig)
    {
      _abilitiesConfig = abilitiesConfig;
      _effectsConfig = effectsConfig;
    }

    AbilityStats IAbilityStatsProvider.GetAbilityStats(string abilityName)
    {
      AbilityData abilityData = _abilitiesConfig.Data[abilityName];

      List<EffectSetup> effectSetups = new();

      foreach (var effect in abilityData.Effects)
      {
        EffectData effectData = _effectsConfig.Data[effect];

        effectSetups.Add(new EffectSetup
        {
          EffectTypeId = effectData.EffectTypeId,
          Value = effectData.Value
        });
      }

      return new AbilityStats
      {
        AbilityName = abilityName,
        EffectSetups = effectSetups,
      };
    }
  }
}
  
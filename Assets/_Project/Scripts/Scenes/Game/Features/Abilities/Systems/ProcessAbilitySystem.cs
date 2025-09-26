using _Project.Scripts.Game.Features.Abilities._Configs.Data;
using _Project.Scripts.Game.Features.Abilities.Components;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Game.Features.Abilities.Systems
{
  public class ProcessAbilitySystem : SystemComponent<AbilityComponent>
  {
    private IAbilityStatsProvider _abilityStatsProvider;

    [Inject]
    private void Construct(IAbilityStatsProvider abilityStatsProvider)
    {
      _abilityStatsProvider = abilityStatsProvider;
    }
    
    protected override void OnUpdate()
    {
      base.OnUpdate();
      
      Components.Foreach(Process);
    }

    private void Process(AbilityComponent abilityComponent)
    {
      DebugLogger.Log("Process ability: " + abilityComponent.AbilityName, LogsType.Ability);

      if (abilityComponent.Unit != null)
      {
        AbilityStats abilityStats = _abilityStatsProvider.GetAbilityStats(abilityComponent.AbilityName);
        foreach (var effect in abilityStats.EffectSetups)
        {
          ProcessEffect(effect, abilityComponent.Unit);
        }
      }
      
      abilityComponent.Remove();
    }
    
    private void ProcessEffect(EffectSetup effect, IUnit unit)
    {
      switch(effect.EffectTypeId)
      {
        case EffectTypeId.Damage:
          unit.Health.CurrentHealth.Value = Mathf.Max(0, unit.Health.CurrentHealth.Value - effect.Value);
          break;
        case EffectTypeId.Heal:
          unit.Health.CurrentHealth.Value = Mathf.Min(unit.Health.MaxHealth, unit.Health.CurrentHealth.Value + effect.Value);
          break;
      }
    }
  }
}
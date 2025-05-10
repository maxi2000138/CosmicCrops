using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.Abilities._Configs.Data;
using _Project.Scripts.Game.Features.Abilities.Components;
using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Infrastructure.Logger;
using _Project.Scripts.Infrastructure.Systems;
using _Project.Scripts.Utils.Extensions;
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

      if (abilityComponent.Target != null)
      {
        AbilityStats abilityStats = _abilityStatsProvider.GetAbilityStats(abilityComponent.AbilityName);
        foreach (var effect in abilityStats.EffectSetups)
        {
          ProcessEffect(effect, abilityComponent.Target);
        }
      }
      
      abilityComponent.Remove();
    }
    
    private void ProcessEffect(EffectSetup effect, ITarget target)
    {
      switch(effect.EffectTypeId)
      {
        case EffectTypeId.Damage:
          target.Health.CurrentHealth.Value -= effect.Value;
          break;
        case EffectTypeId.Heal:
          target.Health.CurrentHealth.Value += effect.Value;
          break;
      }
    }
  }
}
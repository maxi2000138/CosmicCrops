using _Project.Scripts.Game.Features.Abilities._Configs.Data;

namespace _Project.Scripts.Game.Features.Abilities.Services
{
  public interface IAbilityStatsProvider
  {
    AbilityStats GetAbilityStats(string abilityName);
  }
}
namespace _Project.Scripts.Game.Features.Abilities.Services
{
  public interface IAbilityStatsProvider
  {
    AbilityStats GetAbilityStats(string abilityName);
  }
}
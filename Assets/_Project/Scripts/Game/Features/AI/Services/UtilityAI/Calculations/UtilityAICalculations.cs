using _Project.Scripts.Game.Features.Abilities.Services;
using _Project.Scripts.Game.Features.AI.UtilityAI.Calculations;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI.Calculations
{
  public class UtilityAICalculations
  {
    public When When { get; }
    public GetInput GetInput { get; }
    public Score Score { get; }
    
    public UtilityAICalculations(IAbilityStatsProvider abilityStatsProvider)
    {
      When = new When(abilityStatsProvider);
      GetInput = new GetInput();
      Score = new Score();
    }
  }
}
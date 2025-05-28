using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.AIReporter;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI.Calculations;
using _Project.Scripts.Game.Features.AI.UtilityAI;
using _Project.Scripts.Game.Features.UtilityAI;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI
{
  public class UtilityAI : IArtificialIntelligence
  {
    private readonly IEnumerable<IUtilityFunction> _utilityFunctions;

    private readonly ITargetPicker _targetPicker;
    private readonly IAIReporter _aiReporter;

    public UtilityAI(ITargetPicker targetPicker, IAIReporter aiReporter, UtilityAICalculations utilityAICalculations)
    {
      _targetPicker = targetPicker;
      _aiReporter = aiReporter;
      _utilityFunctions = new Brains(utilityAICalculations).GetUtilityFunctions();
    }
    
    public UnitAction MakeBestDecision(EnemyComponent enemy)
    {
      List<ScoredAction> choises = GetScoredUnitActions(enemy).ToList();
      
      _aiReporter.ReportDecisionScores(enemy, choises);

      return choises.FindMax(x => x.Score);
    }

    private IEnumerable<ScoredAction> GetScoredUnitActions(EnemyComponent enemy)
    {
      foreach (BattleAction battleAction in BattleActions(enemy))
      {
        var score = CalculateScore(battleAction, enemy);
        
        yield return new ScoredAction(battleAction, score);
      }
    }

    private IEnumerable<BattleAction> BattleActions(EnemyComponent enemy)
    {
      foreach (UnitAction action in enemy.AI.Actions)
      {
        foreach (IUnit target in _targetPicker.AvailableTargetsFor(action, enemy))
        {
          yield return new BattleAction()
          {
            ActionType = action.ActionType,
            Unit = target, 
          };
        }
      }
    }

    private float CalculateScore(BattleAction battleAction, EnemyComponent enemy)
    {
      List<ScoreFactor> scoreFactors = (from utilityFunction in _utilityFunctions
        where utilityFunction.AppliesTo(battleAction, enemy)
          let input = utilityFunction.GetInput(battleAction, enemy) 
          let score = utilityFunction.Score(input, enemy)
            
        select new ScoreFactor(utilityFunction.Name, score)).ToList();
      
      _aiReporter.ReportDecisionDetails(enemy, battleAction, scoreFactors);

      return scoreFactors.Sum(x => x.Score);
    }
  }
}
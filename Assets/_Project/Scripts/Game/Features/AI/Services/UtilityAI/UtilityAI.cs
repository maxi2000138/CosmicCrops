using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.AIReporter;
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

    public UtilityAI(ITargetPicker targetPicker, IAIReporter aiReporter)
    {
      _targetPicker = targetPicker;
      _aiReporter = aiReporter;
      _utilityFunctions = new Brains().GetUtilityFunctions();
    }
    
    public UnitAction MakeBestDecision(UnitComponent unit)
    {
      List<ScoredAction> choises = GetScoredUnitActions(unit).ToList();
      
      _aiReporter.ReportDecisionScores(unit, choises);

      return choises.FindMax(x => x.Score);
    }

    private IEnumerable<ScoredAction> GetScoredUnitActions(UnitComponent unit)
    {
      foreach (BattleAction battleAction in BattleActions(unit))
      {
        var score = CalculateScore(battleAction, unit);
        
        yield return new ScoredAction(battleAction, score);
      }
    }

    private IEnumerable<BattleAction> BattleActions(UnitComponent unit)
    {
      foreach (UnitAction action in unit.AI.Actions)
      {
        foreach (ITarget target in _targetPicker.AvailableTargetsFor(action, unit))
        {
          yield return new BattleAction()
          {
            Producer = unit,
            ActionType = action.ActionType,
            Target = target, 
          };
        }
      }
    }

    private float CalculateScore(BattleAction battleAction, UnitComponent unit)
    {
      List<ScoreFactor> scoreFactors = (from utilityFunction in _utilityFunctions
        where utilityFunction.AppliesTo(battleAction, unit)
          let input = utilityFunction.GetInput(battleAction, unit) 
          let score = utilityFunction.Score(input, unit)
            
        select new ScoreFactor(utilityFunction.Name, score)).ToList();
      
      _aiReporter.ReportDecisionDetails(battleAction, unit, scoreFactors);

      return scoreFactors.Sum(x => x.Score);
    }
  }
}
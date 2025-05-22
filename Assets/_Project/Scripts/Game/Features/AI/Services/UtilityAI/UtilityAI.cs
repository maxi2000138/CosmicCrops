using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.UtilityAI;
using _Project.Scripts.Utils.Extensions;

namespace _Project.Scripts.Game.Features.AI.UtilityAI
{
  public class UtilityAI : IArtificialIntelligence
  {
    private readonly IEnumerable<IUtilityFunction> _utilityFunctions;

    private readonly LevelModel _levelModel;
    private readonly ITargetPicker _targetPicker;

    public UtilityAI(LevelModel levelModel, ITargetPicker targetPicker)
    {
      _levelModel = levelModel;
      _targetPicker = targetPicker;
      _utilityFunctions = new Brains().GetUtilityFunctions();
    }
    
    public UnitAction MakeBestDecision(IEnemy enemy)
    {
      var choises = GetScoredUnitActions(enemy);

      return choises.FindMax(x => x.Score);
    }

    private IEnumerable<ScoredAction> GetScoredUnitActions(IEnemy enemy)
    {
      foreach (BattleAction battleAction in BattleActions(enemy))
      {
        var score = CalculateScore(battleAction, enemy);
        
        yield return new ScoredAction(battleAction, score);
      }
    }

    private IEnumerable<BattleAction> BattleActions(IEnemy enemy)
    {
      foreach (UnitAction action in enemy.Actions)
      {
        foreach (ITarget target in _targetPicker.AvailableTargetsFor(action, enemy))
        {
          yield return new BattleAction()
          {
            Producer = enemy,
            ActionType = action.ActionType,
            Target = target, 
          };
        }
      }
    }

    private float CalculateScore(BattleAction battleAction, IEnemy enemy)
    {
      IEnumerable<ScoreFactor> scoreFactors = (from utilityFunction in _utilityFunctions
        where utilityFunction.AppliesTo(battleAction, enemy)
          let input = utilityFunction.GetInput(battleAction, enemy) 
          let score = utilityFunction.Score(input, enemy)
            
        select new ScoreFactor(utilityFunction.Name, score));

      return scoreFactors.Sum(x => x.Score);
    }
  }
}
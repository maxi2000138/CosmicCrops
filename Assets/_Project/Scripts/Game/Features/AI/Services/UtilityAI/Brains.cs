using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.AI.UtilityAI.Calculations;

namespace _Project.Scripts.Game.Features.AI.UtilityAI 
{
  public class Brains
  {
    private Convolutions _convolutions = new Convolutions()
    {
      { When.ActionIsPursuit, GetInput.DistanceToTarget, Score.AsIs, "Pursuit" },
    };
    
    public IEnumerable<IUtilityFunction> GetUtilityFunctions()
    {
      return _convolutions;
    }
  }

  public class Convolutions : List<UtilityFunction>
  {
    public void Add(
      Func<BattleAction, IEnemy, bool> appliesTo,
      Func<BattleAction, IEnemy, float> getInput, 
      Func<float, IEnemy, float> score,
      string name)
    {
      Add(new UtilityFunction(appliesTo, getInput, score, name));
    }
  }
}
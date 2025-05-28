using System;
using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI.Calculations;
using _Project.Scripts.Game.Features.AI.UtilityAI.Calculations;
using static _Project.Scripts.Utils.UtilityAI.ConditionCombiner;

namespace _Project.Scripts.Game.Features.AI.UtilityAI 
{
  public class Brains
  {
    private readonly GetInput GetInput;
    private readonly Score Score;
    private readonly When When;
    
    private Convolutions _convolutions;
    
    public Brains(UtilityAICalculations calculations)
    {
      When = calculations.When;
      GetInput = calculations.GetInput;
      Score = calculations.Score;

      _convolutions = new Convolutions()
      {
        { When.ActionIsPatrol, GetInput.One, Score.ScaleBy(10f), "Base patrol weight" },
        
        { AllOf(When.ActionIsPursuit, When.TargetInPursuitRange, When.TargetIsCharacter), GetInput.One, Score.ScaleBy(15f), "Pursuit character in pursuit range" },
        { AllOf(When.ActionIsPursuit, When.TargetIsEnemy, When.TargetWantsToHeal), GetInput.One, Score.ScaleBy(25f), "Pursuit enemy to heal self" },
        { AllOf(When.ActionIsPursuit, When.WeaponHealing, When.TargetIsEnemy), GetInput.TargetPercentageHealth, Score.CullByPercentageHealth(25f), "Pursuit enemy to heal him" },

        { AllOf(When.ActionIsFight, When.TargetInAttackRange, When.WeaponDamaging, When.TargetIsCharacter), GetInput.One, Score.ScaleBy(35f), "Damage character attack" },
        { AllOf(When.ActionIsFight, When.TargetInAttackRange, When.WeaponHealing,  When.TargetIsEnemy), GetInput.TargetPercentageHealth, Score.CullByPercentageHealth(75f), "heal enemy attack" },
      };
    }
    
    public IEnumerable<IUtilityFunction> GetUtilityFunctions()
    {
      return _convolutions;
    }
  }

  public class Convolutions : List<UtilityFunction>
  {
    public void Add(
      Func<BattleAction, EnemyComponent, bool> appliesTo,
      Func<BattleAction, EnemyComponent, float> getInput, 
      Func<float, EnemyComponent, float> score,
      string name)
    {
      Add(new UtilityFunction(appliesTo, getInput, score, name));
    }
  }
}
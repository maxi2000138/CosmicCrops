using System;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI
{
  public class UtilityFunction : IUtilityFunction
  {
    private readonly Func<BattleAction, EnemyComponent, bool> _appliesTo;
    private readonly Func<BattleAction, EnemyComponent, float> _getInput;
    private readonly Func<float, EnemyComponent, float> _score;

    public string Name { get; }
    
    public UtilityFunction(Func<BattleAction, EnemyComponent, bool> appliesTo, Func<BattleAction, EnemyComponent, float> getInput,
      Func<float, EnemyComponent, float> score,
      string name)
    {
      _appliesTo = appliesTo;
      _getInput = getInput;
      _score = score;
      Name = name;
    }
    
    public bool AppliesTo(BattleAction battleAction, EnemyComponent enemy) => _appliesTo(battleAction, enemy);
    public float GetInput(BattleAction battleAction, EnemyComponent enemy) => _getInput(battleAction, enemy);
    public float Score(float input, EnemyComponent enemy) => _score(input, enemy);
  }
}
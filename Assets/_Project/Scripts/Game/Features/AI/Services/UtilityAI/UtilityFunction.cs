using System;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI
{
  public class UtilityFunction : IUtilityFunction
  {
    private readonly Func<BattleAction, IEnemy, bool> _appliesTo;
    private readonly Func<BattleAction, IEnemy, float> _getInput;
    private readonly Func<float, IEnemy, float> _score;

    public string Name { get; }
    
    public UtilityFunction(Func<BattleAction, IEnemy, bool> appliesTo, Func<BattleAction, IEnemy, float> getInput,
      Func<float, IEnemy, float> score,
      string name)
    {
      _appliesTo = appliesTo;
      _getInput = getInput;
      _score = score;
      Name = name;
    }
    
    public bool AppliesTo(BattleAction battleAction, IEnemy enemy) => _appliesTo(battleAction, enemy);
    public float GetInput(BattleAction battleAction, IEnemy enemy) => _getInput(battleAction, enemy);
    public float Score(float input, IEnemy enemy) => _score(input, enemy);
  }
}
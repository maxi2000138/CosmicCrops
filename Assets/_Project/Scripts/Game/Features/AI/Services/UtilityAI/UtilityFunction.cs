using System;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Components;
using _Project.Scripts.Game.Features.AI.Services.UtilityAI;

namespace _Project.Scripts.Game.Features.AI.UtilityAI
{
  public class UtilityFunction : IUtilityFunction
  {
    private readonly Func<BattleAction, UnitComponent, bool> _appliesTo;
    private readonly Func<BattleAction, UnitComponent, float> _getInput;
    private readonly Func<float, UnitComponent, float> _score;

    public string Name { get; }
    
    public UtilityFunction(Func<BattleAction, UnitComponent, bool> appliesTo, Func<BattleAction, UnitComponent, float> getInput,
      Func<float, UnitComponent, float> score,
      string name)
    {
      _appliesTo = appliesTo;
      _getInput = getInput;
      _score = score;
      Name = name;
    }
    
    public bool AppliesTo(BattleAction battleAction, UnitComponent unit) => _appliesTo(battleAction, unit);
    public float GetInput(BattleAction battleAction, UnitComponent unit) => _getInput(battleAction, unit);
    public float Score(float input, UnitComponent unit) => _score(input, unit);
  }
}
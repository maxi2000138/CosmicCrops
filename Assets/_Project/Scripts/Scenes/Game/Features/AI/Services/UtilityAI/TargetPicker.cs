using System.Collections.Generic;
using _Project.Scripts.Game.Features.Level.Model;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Units.Enemy.Actions;
using Sirenix.Utilities;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI
{
  public class TargetPicker : ITargetPicker
  {
    private readonly LevelModel _levelModel;
    
    public TargetPicker(LevelModel levelModel)
    {
      _levelModel = levelModel;
    }

    public IEnumerable<IUnit> AvailableTargetsFor(UnitAction action, IUnit producer)
    {
      ISet<IUnit> targets = new HashSet<IUnit>();
      
      void AddEnemies() => _levelModel.Enemies.ForEach(x => targets.Add(x));
      void AddCharacter() => targets.Add(_levelModel.Character);
      void AddSelf() => targets.Add(producer);
      void ExlcudeSelf() => targets.Remove(producer);

      switch (action.TargetType)
      {
        case TargetType.Self:
          AddSelf();
          break;
        case TargetType.EnemyOrCharacter:
          AddEnemies();
          AddCharacter();
          ExlcudeSelf();
          break;
      }
      
      return targets;
    }
  }
}
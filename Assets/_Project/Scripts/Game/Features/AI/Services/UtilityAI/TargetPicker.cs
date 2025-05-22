using System.Collections.Generic;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Features.AI.UtilityAI;
using _Project.Scripts.Game.Features.Level.Model;
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

    public IEnumerable<ITarget> AvailableTargetsFor(UnitAction action, ITarget producer)
    {
      ISet<ITarget> targets = new HashSet<ITarget>();
      
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
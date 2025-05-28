using System.Collections.Generic;
using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Enemy.Actions;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI
{
  public interface ITargetPicker
  {
    IEnumerable<IUnit> AvailableTargetsFor(UnitAction action, IUnit producer);
  }
}
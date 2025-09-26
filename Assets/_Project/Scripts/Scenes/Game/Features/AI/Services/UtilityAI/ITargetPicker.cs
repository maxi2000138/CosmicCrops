using System.Collections.Generic;
using _Project.Scripts.Game.Features.Units._Interfaces;
using _Project.Scripts.Game.Features.Units.Enemy.Actions;

namespace _Project.Scripts.Game.Features.AI.Services.UtilityAI
{
  public interface ITargetPicker
  {
    IEnumerable<IUnit> AvailableTargetsFor(UnitAction action, IUnit producer);
  }
}
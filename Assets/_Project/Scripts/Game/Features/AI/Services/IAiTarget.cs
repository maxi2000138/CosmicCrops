using System.Collections.Generic;
using _Project.Scripts.Game.Entities.Unit.Actions;

namespace _Project.Scripts.Game.Features.UtilityAI
{
  public interface IAiTarget
  {
    public IEnumerable<UnitAction> Actions { get; }
  }
}
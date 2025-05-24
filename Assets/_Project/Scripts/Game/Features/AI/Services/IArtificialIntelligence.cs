using _Project.Scripts.Game.Entities._Interfaces;
using _Project.Scripts.Game.Entities.Unit.Actions;
using _Project.Scripts.Game.Entities.Unit.Components;

namespace _Project.Scripts.Game.Features.UtilityAI
{
  public interface IArtificialIntelligence
  {
    UnitAction MakeBestDecision(UnitComponent unit);
  }
}
namespace _Project.Scripts.Game.Features.Units.Enemy.Actions
{
  public enum UnitActionType : byte
  {
    Patrol     = 0,
    Pursuit    = 1,
    Fight      = 2,
    
    None       = byte.MaxValue
  }
}
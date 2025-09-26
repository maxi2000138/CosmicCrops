namespace _Project.Scripts.Game.Features.Units._Interfaces
{
  public interface IUnit : IPosition, IHealth, IHeight, IStateMachineComponent, IWeaponMediatorComponent
  {
    public IUnit Target { get; }
  }
}
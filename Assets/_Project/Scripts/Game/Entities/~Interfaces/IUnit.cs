namespace _Project.Scripts.Game.Entities._Interfaces
{
  public interface IUnit : IPosition, IHealth, IHeight, IStateMachineComponent, IWeaponMediatorComponent
  {
    public IUnit Target { get; }
  }
}